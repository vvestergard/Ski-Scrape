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
            
            HtmlDocument page = new HtmlWeb().Load("http://www.powder.com/photo-of-the-day/");
                var aTags = page.DocumentNode.SelectNodes("//article[@class='standard-article']//img");
                int count = aTags.Count();
                List<string> tags = new List<string>();
                
                foreach (var node in aTags)
                {
                    var fileName = node.Attributes["data-srcset"].Value;
                    string[] splits = fileName.Split(' ');
                    
                    tags.Add(splits[0]);

                }
                tags.ToArray();
                ViewBag.link = tags;
                ViewBag.antal = count;
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