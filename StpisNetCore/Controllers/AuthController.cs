using Microsoft.AspNetCore.Mvc;
using StpisNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StpisNetCore.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelsContext _dbContext;

        static public string HashPassword(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public AuthController(ModelsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Login, string Password)
        {
            tblClients Client = _dbContext.clients.Where(x => x.Login == Login).FirstOrDefault<tblClients>();
            if(Client.Id != 0 && Client.Password != null)
            {
                string EncPassword = HashPassword(Password);
                if (Client.Password.Equals(EncPassword))
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(string Login, string Password, string PhoneNumber)
        {
            Microsoft.Extensions.Primitives.StringValues CheckPas = "";
            Request.Form.TryGetValue("CheckPassword", out CheckPas);
            if (!Password.Equals(CheckPas))
            {
                ViewBag.IncorectData = "Passwords does not match";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
    }
}
