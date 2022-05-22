using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Controllers
{
    public class ClientController : Controller
    {
        private readonly ModelsContext _dbContext;

        public ClientController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var ClientsList = _dbContext.clients.ToList();
            GetInfo();
            IList<tblClients> ClientsViewList = ClientsList;
            return View(ClientsViewList);
        }

        public List<tblClients> GetInfo()
        {
            List<tblClients> Clients =  _dbContext.clients.Join(_dbContext.role, client => client.Role_Id, role => role.Id, (client, role) => new tblClients
            {
                Id = client.Id,
                Address = client.Address,
                Login = client.Login,
                Password = client.Password,
                PhoneNumber = client.PhoneNumber,
                Role_Id = role.Id,
                Role = role
            }).ToList();
            return Clients;
        }

        [HttpGet]
        public IActionResult Create()
        {
            tblClients Client = new tblClients();
            Client.RolesList = _dbContext.role.ToList();
            return View(Client);
        }

        [HttpPost]
        public IActionResult Create(tblClients Client)
        {
            if (Client.Id == 0)
            {
                string EncPassword = AuthController.HashPassword(Client.Password);
                Client.Password = EncPassword;
                _dbContext.clients.Add(Client);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            tblClients Client = _dbContext.clients.Where(x => x.Id == id).FirstOrDefault<tblClients>();
            Client.RolesList = _dbContext.role.ToList();
            return View(Client);
        }

        [HttpPost]
        public IActionResult Edit(tblClients Clients)
        {
            _dbContext.Entry(Clients).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            tblClients Client = _dbContext.clients.Where(x => x.Id == id).FirstOrDefault<tblClients>();
            _dbContext.clients.Remove(Client);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
