﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollectorExercise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

             if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Customer");
            }
            else if (User.IsInRole("Employee"))
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}