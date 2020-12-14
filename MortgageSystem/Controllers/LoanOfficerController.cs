using MortgageSystem.Filters;
using MortgageSystem.Models;
using MortgageSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace MortgageSystem.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class LoanOfficerController : Controller
    {
        MortgageDBContext context = new MortgageDBContext();
        // GET: LoanOfficer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplyLoan()
        {
            ViewBag.Date = context.Mortgages.ToList();

            

            return View();
        }

        [HttpPost]
        public ActionResult ApplyLoan(CustomerLoanMortage obj)
        {
            ViewBag.Date = context.Mortgages.ToList();

            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }
                var customer = context.Customers.Where(c => c.FName == obj.FName && c.LName == obj.LName &&

                                                            c.Contact == obj.Contact && c.Email == obj.Email

                                                        ).FirstOrDefault();

                if (customer == null)
                {
                    ModelState.AddModelError("", "Please Enter correct Customer Information");
                    return View();
                }

                obj.LoanNumber = LoanNumber(obj);

                obj.LoanApplyDate = DateTime.Now;
                obj.DayOfEmiPayment = new DateTime(year, month, 5).AddMonths(1);
                obj.LoanStatus = "new";





                context.CustomerLoanMortages.Add(obj);
                context.SaveChanges();

            }
            catch(Exception ex)
            {
                Session.Add("ErrorMessage", ex.Message);
                return View("ErrorView");
            }
            var loanId = context.CustomerLoanMortages.Where(loan => loan.LoanNumber == obj.LoanNumber).FirstOrDefault().Id;
           

            


            

            return RedirectToAction("Index");
        }

        public ActionResult LoanStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoanStatus(LoanStatus loanStatus)
        {
            List<CustomerLoanMortage> loanList = null;
            if (!ModelState.IsValid)
            {
                return View();
            }



            loanList = context.CustomerLoanMortages.Where(c => c.Contact.ToString() == loanStatus.Status).ToList();


            if (loanList.Count == 0)
            {
                ModelState.AddModelError("", "Enter a Valid Credentials !!");
                return View();
            }

            return RedirectToAction("FewInfoLoan", new { id = loanStatus.Status });
        }
        public ActionResult FewInfoLoan(string id)
        {
            IEnumerable<CustomerLoanMortage> loanList = null;


            loanList = context.CustomerLoanMortages.Where(c => c.Contact.ToString() == id).ToList();


            return View(loanList);
        }
        public ActionResult ShowAllDetails(string LoanNumber)
        {
            var loanDetails = context.CustomerLoanMortages.Where(c => c.LoanNumber == LoanNumber).FirstOrDefault();

            return View(loanDetails);
        }


        public ActionResult ViewMortgage()
        {
            var model = context.Mortgages.ToList();
            return View(model);
        }
   

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        public string LoanNumber(CustomerLoanMortage obj)
        {
            Random random = new Random();
            var details = context.Customers.Where(c => c.Contact == obj.Contact).FirstOrDefault();

            var userId = details.UserId;
            var Rnum = random.Next(1000, 10000);

            var loanNumber = userId + Rnum;

            return loanNumber;
        }

    }
}