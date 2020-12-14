using MortgageSystem.Filters;
using MortgageSystem.Models;
using MortgageSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MortgageSystem.Controllers
{
    [ExceptionHandler]
    public class OfficerController : Controller
    {
        MortgageDBContext context = new MortgageDBContext();
        // GET: Officer
        [Authorize]
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginUser loginUser)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var officerList = context.Officers.ToList();

            var isExist = context.Officers.Any(o => o.UserId == loginUser.UserId && o.Password == loginUser.Password && o.UserType==loginUser.UserType);

            if(isExist)
            {


                //
                var person = context.Officers.Where(s => s.UserId == loginUser.UserId).FirstOrDefault();
                string name ="Welcome - " +person.FName+" " +person.LName;
                FormsAuthentication.SetAuthCookie(name, false);
                if (loginUser.UserType == "Financial")
                {

                    //FormsAuthentication.SetAuthCookie(L_name, false);
                    return View("Financial");
                }
                else
                {
                    Session["Name"] = person.FName + " " + person.LName;
                   // FormsAuthentication.SetAuthCookie(L_name, false);
                    return View("Loan");
                }

                
            }
            ModelState.AddModelError("", "User Id and Password is Incorrent !!");
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Officer officer)
        {
            bool flag = false;

            if (!ModelState.IsValid)
            {
                return View(officer);
            }

            var isExist = IsExist(officer.UserId, officer.Contact, officer.Email);
            if (isExist["userId"])
            {
                ModelState.AddModelError("userExist", "This User Id is already in use");
                flag = true;
            }
            if (isExist["contact"])
            {
                ModelState.AddModelError("contactExist", "This Phone Number is already in use");
                flag = true;
            }
            if (isExist["mail"])
            {
                ModelState.AddModelError("mailExist", "This Email is already in use");
                flag = true;
            }

            if (flag)
            {
                return View();
            }
            try
            {
                context.Officers.Add(officer);
                context.SaveChanges();
                ModelState.Clear();

            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            return View("SignIn");

           
        }
        //Loan starts here
        [Authorize]
        public ActionResult Loan()
        {

            return View();
        }

        //Financial starts here

        [Authorize]
        public ActionResult Financial()
        {
            return View();
        }

        //user ID and Password Recovery

        public ActionResult ForgetUserId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetUserId(ForgetUseId forgetUseId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var IsExist = context.Officers.Where(c => c.Email == forgetUseId.EmailId && c.Contact == forgetUseId.Contact).FirstOrDefault();
            if (IsExist == null)
            {
                ModelState.AddModelError("", "Please Enter Valid Email and Password");
                return View();
            }

            ViewBag.UserId = IsExist.UserId;
            ModelState.Clear();
            return View();
        }



        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword forgetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var IsValid = context.Officers.Where(c => c.SecurityAnimal == forgetPassword.SecurityAnimal &&
                                                        c.SecurityBirthPlace == forgetPassword.SecurityBirthPlace &&
                                                         c.SecurityNumber == forgetPassword.SecurityNumber).FirstOrDefault();

            if (IsValid == null)
            {
                ModelState.AddModelError("", "Please Enter the Valid Details !!");
                return View();
            }



            return RedirectToAction("PasswordReset");
        }

        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var officer = context.Officers.Where(c => c.UserId == resetPassword.UserId).FirstOrDefault();

            if (officer == null)
            {
                ModelState.AddModelError("", "Please Enter a Valid User Id");
                return View();
            }
            officer.Password = resetPassword.Password;
            officer.ConfirmPass = resetPassword.ConfirmPass;

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }
            ViewBag.ResetPassword = "Password Has Reset";


            return View();
        }

        public Dictionary<string, bool> IsExist(string userID, long Contact, string mail)
        {

            Dictionary<string, bool> alreadyExist = new Dictionary<string, bool>();

            var isUser = context.Officers.Where(u => u.UserId == userID).FirstOrDefault();
            var isContact = context.Officers.Where(u => u.Contact == Contact).FirstOrDefault();
            var isMail = context.Officers.Where(u => u.Email == mail).FirstOrDefault();

            alreadyExist.Add("userId", isUser != null);
            alreadyExist.Add("contact", isContact != null);
            alreadyExist.Add("mail", isMail != null);

            return alreadyExist;
        }
    }
}