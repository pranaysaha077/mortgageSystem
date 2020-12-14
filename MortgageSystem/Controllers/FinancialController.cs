using MortgageSystem.Filters;
using MortgageSystem.Models;
using MortgageSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MortgageSystem.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class FinancialController : Controller
    {
        MortgageDBContext context = new MortgageDBContext();
        // GET: Financial
        public ActionResult AddMortgage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMortgage(Mortgage mortgage)
        {
            var mort = context.Mortgages.Where(m => m.MortgageItem == mortgage.MortgageItem).FirstOrDefault();

            

            if (!ModelState.IsValid)
            {
                return View(mortgage);
            }

           if(mort != null)
            {
                ModelState.AddModelError("", "This Item Is Already Present .");
                return View();
            }

            mortgage.UpdatedDate = DateTime.Now;

            try
            {
                context.Mortgages.Add(mortgage);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            ModelState.Clear();

            ViewBag.msg = 1;
            return View();
        }

        public ActionResult EditMortgage()
        {
            var mortgageList = context.Mortgages.ToList();

            return View(mortgageList);
        }
        public ActionResult Edit(int id)
        {
            var mortgage = context.Mortgages.Find(id);

            

            return View(mortgage);
        }

        [HttpPost]
        public ActionResult Edit(Mortgage mortgage)
        {
            var nmort = context.Mortgages.Find(mortgage.Id);
            try
            {
                nmort.MortgageItem = mortgage.MortgageItem;

                nmort.ValueType = mortgage.ValueType;
                nmort.MortgageValue = mortgage.MortgageValue;
                nmort.MortgageInterest = mortgage.MortgageInterest;
                nmort.UpdatedDate = DateTime.Now;

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            return RedirectToAction("Financial","Officer");
        }

        public ActionResult LoanApplication()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoanApplication(LoanStatus loanStatus)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(loanStatus.Status == "New")
            {
                return RedirectToAction("New");
            }
            else if(loanStatus.Status == "Approve")
            {
                return RedirectToAction("ApproveList");
            }
            else
            {
                return RedirectToAction("DeniedList");
            }
        }

        public ActionResult New()
        {
            var new_application = context.CustomerLoanMortages.Where(c => c.LoanStatus.Equals("new")).ToList();

            return View(new_application);
        }

            public ActionResult ShowAllDetails(string LoanNumber)
            {
                var loanDetails = context.CustomerLoanMortages.Where(c => c.LoanNumber == LoanNumber).FirstOrDefault();    
            
                return View(loanDetails);
            }
           

        public ActionResult Approve(int id)
        {

            var application = context.CustomerLoanMortages.Find(id);

            try
            {
                application.LoanStatus = "approve";
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            //emi calculation
            var p = application.Amount;
            var r = application.InterestRate / 1200;
            var n = application.LoanTenure * 12;

            var emiAmt = (p * r * Math.Pow((1+r),n)) /(Math.Pow((1+r),n)-1);

            var totalAmt = emiAmt * n;

            //date calculation
            var date = 5;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;

            DateTime dateTime = new DateTime(year, month, date).AddMonths(1);

            Emi emi = new Emi()
            {
                AmountToPaid = totalAmt,
                MonthlyAmt = emiAmt,
                OutstandingBal = totalAmt,
                LDayOfPayment = null,
                NDayOfPayment = dateTime,
                LoanNumber = application.LoanNumber

            };

            try
            {
                context.Emis.Add(emi);

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }
            
                return RedirectToAction("New");
        }
            
        public ActionResult ApproveList()
        {
            var approveList = context.CustomerLoanMortages.Where(a => a.LoanStatus == "approve").ToList();
            return View(approveList);
        }
        public ActionResult DeniedList()
        {
            var approveList = context.CustomerLoanMortages.Where(a => a.LoanStatus == "denied").ToList();
            return View(approveList);
        }
        public ActionResult ShowApproveDenied(string LoanNumber)
        {
            var loanDetails = context.CustomerLoanMortages.Where(c => c.LoanNumber == LoanNumber).FirstOrDefault();

            return View(loanDetails);
        }


       

         public ActionResult Terms()
        {
            var terms = context.Terms.FirstOrDefault();
            return View(terms);
        }

        public ActionResult EditTerms(int id)
        {
            var term = context.Terms.Find(id);

            return View(term);
        }
        [HttpPost]
        public ActionResult EditTerms(Terms terms)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var term = context.Terms.Find(terms.Id);
            try
            {
                term.Term = terms.Term;
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }

            

            return View(term);
        }

        public ActionResult Denied(int id)
        {
            var application = context.CustomerLoanMortages.Find(id);
            try
            {
                application.LoanStatus = "denied";

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }
            return RedirectToAction("New");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}