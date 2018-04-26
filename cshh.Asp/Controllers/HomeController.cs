using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Controllers
{
    public class HomeController : Controller
    {
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

        public string a()
        {
            return DateTime.Now.ToString()+"<br />";
        }


        //public ActionResult Theme(string themeJq)
        //{
        //    Response.Cookies.Add(new HttpCookie());
        //    return View("Index");
        //}
    }
}