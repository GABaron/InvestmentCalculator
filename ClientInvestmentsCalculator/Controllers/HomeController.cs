﻿using Microsoft.AspNetCore.Mvc;

namespace ClientInvestmentsCalculator.Controllers
{
    public class HomeController : Controller
    {       
        public IActionResult Index()
        {
            return View();
        }            
    }
}
