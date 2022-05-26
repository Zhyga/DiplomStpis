using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ModelsContext _dbContext;

        public OrderController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IList<tblOrders> OrdersViewList = GetInfo();
            return View(OrdersViewList);
        }

        public List<tblOrders> GetInfo()
        {
            List<tblOrders> Orders = _dbContext.orders.Join(_dbContext.products, order => order.ProductId, product => product.Id,
                (order, product) => new
                { order, product }).Join(_dbContext.clients, orderProduct => orderProduct.order.ClientId, client => client.Id,
                (orderProduct, client) => new tblOrders
                {
                    Id = orderProduct.order.Id,
                    OrderNumber = orderProduct.order.OrderNumber,
                    ProductAmount = orderProduct.order.ProductAmount,
                    ProductId = orderProduct.order.ProductId,
                    Product = orderProduct.product,
                    ClientId = orderProduct.order.ClientId,
                    Client = client
                }).ToList();
            return Orders;
        }

        [HttpGet]
        public IActionResult Create()
        {
            tblOrders Order = new tblOrders();
            Order.ClientList = _dbContext.clients.ToList();
            Order.ProductList = _dbContext.products.ToList();
            return View(Order);
        }

        [HttpPost]
        public IActionResult Create(tblOrders Order)
        {
            if (Order.Id == 0)
            {
                _dbContext.orders.Add(Order);
                _dbContext.SaveChanges();
            }
            Order.ClientList = _dbContext.clients.ToList();
            Order.ProductList = _dbContext.products.ToList();
            return View(Order);
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            tblOrders Order = _dbContext.orders.Where(x => x.Id == id).FirstOrDefault<tblOrders>();
            Order.ClientList = _dbContext.clients.ToList();
            Order.ProductList = _dbContext.products.ToList();
            return View(Order);
        }

        [HttpPost]
        public IActionResult Edit(tblOrders Order)
        {
            _dbContext.Entry(Order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            tblOrders Order = _dbContext.orders.Where(x => x.Id == id).FirstOrDefault<tblOrders>();
            _dbContext.orders.Remove(Order);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
