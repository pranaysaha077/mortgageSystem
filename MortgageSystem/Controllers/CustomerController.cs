using Microsoft.Ajax.Utilities;
using MortgageSystem.Filters;
using MortgageSystem.Models;
using MortgageSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace MortgageSystem.Controllers
{
    [ExceptionHandler]
    public class CustomerController : Controller
    {
        MortgageDBContext context = new MortgageDBContext();
        // GET: Customer
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Admin admin)
        {
            var isValid = context.Customers.Any(d => d.UserId == admin.UserId && d.Password == admin.Password);

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (isValid)
            {
                var person = context.Customers.Where(c => c.UserId == admin.UserId).FirstOrDefault();
                string name = "Welcome - " + person.FName + " " + person.LName;
                FormsAuthentication.SetAuthCookie(name, false);
               
                ModelState.Clear();

                System.Web.HttpContext.Current.Session["contact"] = person.Contact;
                Session["userId"] = person.UserId;
                Session["Id"] = person.Id;

                //Session("userId", person.UserId);
                

                return RedirectToAction("AltHome");
            }
            ModelState.AddModelError("", "user id and Password is not correct");
            return View();

           
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            try
            {

                bool flag = false;

                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                if ((DateTime.Now.Year - customer.Dob.Year) < 18)
                {
                    ModelState.AddModelError("", "Age should be Above 18");
                    return View();
                }

                var isExist = IsExist(customer.UserId, customer.Contact, customer.Email);
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

                context.Customers.Add(customer);
                context.SaveChanges();
                ModelState.Clear();

            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            return RedirectToAction("SignIn");

           
        }

        [Authorize]
        public ActionResult AltHome()
        {
            return View();
        }

        public ActionResult ForgetUserId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetUserId(ForgetUseId forgetUseId)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var IsExist = context.Customers.Where(c => c.Email == forgetUseId.EmailId && c.Contact == forgetUseId.Contact).FirstOrDefault();
            if(IsExist == null)
            {
                ModelState.AddModelError("", "Please Enter Valid Email and Password");
                return View();
            }

            @ViewBag.UserCon = 1;
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
            if(!ModelState.IsValid)
            {
                return View();
            }
            var IsValid = context.Customers.Where(c => c.SecurityAnimal == forgetPassword.SecurityAnimal &&
                                                        c.SecurityBirthPlace == forgetPassword.SecurityBirthPlace &&
                                                         c.SecurityNumber == forgetPassword.SecurityNumber).FirstOrDefault();

            if(IsValid == null)
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var customer = context.Customers.Where(c => c.UserId == resetPassword.UserId).FirstOrDefault();

                if (customer == null)
                {
                    ModelState.AddModelError("", "Please Enter a Valid User Id");
                    return View();
                }
                customer.Password = resetPassword.Password;
                customer.ConfirmPass = resetPassword.ConfirmPass;

                context.SaveChanges();

                ViewBag.ResetPassword = "Password Has Reset";
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            return View();
        }

        public ActionResult LoanStatus()
        {
            var contact = (long) System.Web.HttpContext.Current.Session["contact"];

            var loanList = context.CustomerLoanMortages.Where(c => c.Contact == contact && c.LoanStatus=="approve").ToList();  
            return View(loanList);
        }

        public ActionResult ShowAllDetails(string LoanNumber)
        {
            var loanDetails = context.CustomerLoanMortages.Where(c => c.LoanNumber == LoanNumber).FirstOrDefault();

            return View(loanDetails);
        }

        public ActionResult PayEmi(string loanNumber)
        {
            var emiDetails = context.Emis.Where(e => e.LoanNumber == loanNumber).FirstOrDefault();

            ViewBag.Amt = emiDetails.MonthlyAmt.ToString("N3").TrimEnd('0');

            return View(emiDetails);
        }

        public ActionResult Payment(int id)
        {
            Session["EmiId"] = id;

            //DateTime dateTime = new DateTime(year,month,)


            return View();

        }

        [HttpPost]
        public ActionResult Payment(CardDetails cardDetails)
        {
            var emiId = Session["EmiId"];

            var emi = context.Emis.Find(emiId);

            var isValid = context.CardDetails.Where(c =>
                                                        DbFunctions.TruncateTime( c.ExpiryDate) == DbFunctions.TruncateTime(cardDetails.ExpiryDate)
                                                   ).FirstOrDefault();

                if (isValid == null)
                {
                    ModelState.AddModelError("", "Enter Valid Card Details");
                    return View();
                }

                if (isValid.Balance >= emi.MonthlyAmt)
                {
                    emi.OutstandingBal -= emi.MonthlyAmt;


                    emi.LDayOfPayment = emi.NDayOfPayment.GetValueOrDefault();

                    emi.NDayOfPayment = emi.NDayOfPayment.GetValueOrDefault().AddMonths(1);



                    context.SaveChanges();

                    return RedirectToAction("AltHome");

                }
                else
                {
                    ModelState.AddModelError("", "Ensufficient Amount ");
                    return View();
                }
            }
            //DateTime dateTime = new DateTime(year,month,)

        
        public ActionResult MortgageInfo()
        {
            var mortgage = context.Mortgages.ToList();

            return View(mortgage);
        }
        public ActionResult Terms()
        {
            var term = context.Terms.FirstOrDefault();

            return View(term);
        }



        public ActionResult LoanTOEmiTracker()
        {
            var contact = (long)System.Web.HttpContext.Current.Session["contact"];
            var loanList = context.CustomerLoanMortages.Where(c => c.Contact == contact && c.LoanStatus == "approve").ToList();
            return View(loanList);
        }


        //modal pop up


        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult Home(Admin model)
        {
            if (ModelState.IsValid)
            {
                // Save it in database

                //Return Success message
                ViewBag.Message = "Blog saved";
                ModelState.Clear();
                return PartialView("_Partial_View");
            }
            return PartialView("_Partial_View", model);
        }

       



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn");
        }











        public Dictionary<string, bool> IsExist(string userID, long Contact, string mail)
        {

            Dictionary<string, bool> alreadyExist = new Dictionary<string, bool>();

            var isUser = context.Customers.Where(u => u.UserId == userID).FirstOrDefault();
            var isContact = context.Customers.Where(u => u.Contact == Contact).FirstOrDefault();
            var isMail = context.Customers.Where(u => u.Email == mail).FirstOrDefault();

            alreadyExist.Add("userId", isUser != null);
            alreadyExist.Add("contact", isContact != null);
            alreadyExist.Add("mail", isMail != null);

            return alreadyExist;
        }
    }
}