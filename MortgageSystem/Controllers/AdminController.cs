using MortgageSystem.Filters;
using MortgageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MortgageSystem.Controllers
{
    [ExceptionHandler]
    public class AdminController : Controller
    {
        MortgageDBContext context = new MortgageDBContext();
        // GET: Admin
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Admin admin)
        {
            if(!ModelState.IsValid)
            {
                return View(admin);
            }

            var isExist = context.Admins.Any(a => a.UserId == admin.UserId && a.Password == admin.Password);

            if(isExist)
            {
                return View("AdminHome");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect UserId and Password !!");
                return View();
            }

        }
        public ActionResult LogOut()
        {
            Session.Clear();
            ModelState.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}