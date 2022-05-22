using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace StpisNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ModelsContext _dbContext;

        public ProductController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var ProductsList = _dbContext.products.ToList();
            IList<tblProducts> ProductViewList = ProductsList;
            return View(ProductViewList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(tblProducts Product)
        {
            Microsoft.Extensions.Primitives.StringValues price = "";
            Request.Form.TryGetValue("Price", out price);
            Product.Price = decimal.Parse(price, System.Globalization.CultureInfo.InvariantCulture);
            if (Product.Id == 0)
            {
                _dbContext.products.Add(Product);
                _dbContext.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            return View(_dbContext.products.Where(x => x.Id == id).FirstOrDefault<tblProducts>());
        }

        [HttpPost]
        public IActionResult Edit(tblProducts Product)
        {
            Microsoft.Extensions.Primitives.StringValues price = "";
            Request.Form.TryGetValue("Price", out price);
            Product.Price = decimal.Parse(price, System.Globalization.CultureInfo.InvariantCulture);
            _dbContext.Entry(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            tblProducts Product = _dbContext.products.Where(x => x.Id == id).FirstOrDefault<tblProducts>();
            _dbContext.products.Remove(Product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
