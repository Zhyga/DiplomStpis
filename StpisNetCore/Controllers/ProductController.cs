using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace StpisNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _dbContext;

        public ProductController(ProductContext dbContext)
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
        public IActionResult Create(tblProducts product)
        {
            //string price = Request.Form.Get("price");
            //product.Price = decimal.Parse(price, System.Globalization.CultureInfo.InvariantCulture);
            if (product.Id == 0)
            {
                _dbContext.products.Add(product);
                _dbContext.SaveChanges();
            }
            return View();
        }
    }
}
