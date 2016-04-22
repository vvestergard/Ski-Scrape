using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace VimmelOrebro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
             HtmlDocument page = new HtmlWeb().Load("http://www.orebroguiden.com/?p=46903");
                var aTags = page.DocumentNode.SelectNodes("//div[@class='ngg-gallery-thumbnail']//a");

                foreach (var node in aTags)
                {
                    var fileName = node.Attributes["href"].Value;
                }
            return View();
        }

        public ActionResult Clubs()
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
    }
}