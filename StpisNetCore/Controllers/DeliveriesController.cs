using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Controllers
{
    public class DeliveriesController : Controller
    {
        private readonly ModelsContext _dbContext;

        public DeliveriesController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IList<tblDeliveries> DeliveriesViewList = GetInfo();
            return View(DeliveriesViewList);
        }

        public List<tblDeliveries> GetInfo()
        {
            List<tblDeliveries> deliveries = _dbContext.deliveries.Join(_dbContext.region, delivery => delivery.RegionId, region => region.Id,
                 (delivery, region) => new { delivery, region }).Join(_dbContext.status, deliveryregion => deliveryregion.delivery.StatusId,
                  status => status.Id, (deliveryregion, status) => new tblDeliveries
                  {
                      Id = deliveryregion.delivery.Id,
                      DeliveryDate = deliveryregion.delivery.DeliveryDate,
                      OrderDate = deliveryregion.delivery.OrderDate,
                      RegionId = deliveryregion.delivery.RegionId,
                      StatusId = deliveryregion.delivery.StatusId,
                      OrderId = deliveryregion.delivery.OrderId,
                      WarehouseId = deliveryregion.delivery.WarehouseId,
                      Status = status,
                      Region = deliveryregion.region
                  }).ToList();
            return deliveries;
        }

        [HttpGet]
        public IActionResult Create()
        {
            tblDeliveries Model = new tblDeliveries();
            Model.StatusList = _dbContext.status.ToList();
            Model.RegionsList = _dbContext.region.ToList();
            Model.WarehousesList = _dbContext.warehouses.ToList();
            Model.OrdersList = _dbContext.orders.ToList();
            return View(Model);
        }

        [HttpPost]
        public IActionResult Create(tblDeliveries Delivery)
        {
            if (Delivery.Id == 0)
            {
                _dbContext.deliveries.Add(Delivery);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            tblDeliveries Model = _dbContext.deliveries.Where(x => x.Id == id).FirstOrDefault<tblDeliveries>();
            Model.StatusList = _dbContext.status.ToList();
            Model.RegionsList = _dbContext.region.ToList();
            Model.WarehousesList = _dbContext.warehouses.ToList();
            Model.OrdersList = _dbContext.orders.ToList();
            return View(Model);
        }

        [HttpPost]
        public IActionResult Edit(tblDeliveries Delivery)
        {
            _dbContext.Entry(Delivery).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            tblDeliveries Delivery = _dbContext.deliveries.Where(x => x.Id == id).FirstOrDefault<tblDeliveries>();
            _dbContext.deliveries.Remove(Delivery);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
