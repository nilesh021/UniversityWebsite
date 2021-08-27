using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityWebsite.Models;
using System.Web.Security;
using System.Collections.Generic;
using System;

namespace UniversityWebsite.Controllers
{
    public class HomeController : Controller
    {
        private MyDBContext db = new MyDBContext();
        
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["id"] = null;
            HttpContext.Session.Abandon();
            return RedirectToAction("Index", "Home");
        }




    }
 }