﻿using MortgageSystem.Filters;
using System.Web;
using System.Web.Mvc;

namespace MortgageSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandler());
        }
    }
}
