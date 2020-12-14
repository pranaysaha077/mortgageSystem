using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MortgageSystem.Filters
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            HttpContext.Current.Session.Add("ErrorMessage", e.Message);
            filterContext.Result = new ViewResult()
            {
                ViewName = "ErrorView"
            };

        }
    }
}