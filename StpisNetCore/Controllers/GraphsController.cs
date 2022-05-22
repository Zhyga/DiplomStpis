using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace StpisNetCore.Controllers
{
    public class GraphsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ModelsContext _dbContext;

        public GraphsController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewData["Clients"] = _dbContext.clients.ToList();
            ViewData["Deliveries"] = _dbContext.deliveries.ToList();
            ViewData["Regions"] = _dbContext.region.ToList();
            return View();
        }

        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public Microsoft.AspNetCore.Mvc.ActionResult GetValues()
        //{
        //    List<tblClients> clients =  _dbContext.clients.ToList();
        //    List<tblDeliveries> deliveries = _dbContext.deliveries.ToList();
        //    List<tblRegions> regions = _dbContext.region.ToList();
        //    var Temp = Json(new { clients, deliveries, regions }, JsonRequestBehavior.AllowGet);
        //    return Temp;
        //}
    }
}
