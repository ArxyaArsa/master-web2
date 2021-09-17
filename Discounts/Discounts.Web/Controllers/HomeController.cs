using Discounts.Web.Helpers;
using Discounts.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Start");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Start()
        {
            if (User.IsInRole(WebConstants.AdminRole))
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            else if (User.IsInRole(WebConstants.PartnerRole))
                return RedirectToAction("Index", "Main", new { area = "Partner" });
            else if (User.IsInRole(WebConstants.UserRole))
                return RedirectToAction("ViewByCategory", "Main", new { area = "User" });
            else
                return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
    }
}
