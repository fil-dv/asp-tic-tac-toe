﻿using System.Web;
using System.Web.Mvc;

namespace Web_Tic_tac_toe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
