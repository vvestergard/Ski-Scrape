using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VimmelOrebro.Models;

namespace VimmelOrebro.Controllers
{
    public class BildersController : Controller
    {
        private VimmelOrebroContext db = new VimmelOrebroContext();

        // GET: Bilders
        public ActionResult Index()
        {
            return View(db.Bilders.ToList());
        }

        // GET: Bilders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bilder bilder = db.Bilders.Find(id);
            if (bilder == null)
            {
                return HttpNotFound();
            }
            return View(bilder);
        }

        // GET: Bilders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bilders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BID,bildPath,EnrollmentDate")] Bilder bilder, HttpPostedFileBase bildPath,string namn)
        {
            if (ModelState.IsValid)
            {
                var sida = namn;
                string sidan = sida;
                HtmlDocument page = new HtmlWeb().Load(sidan);
                HtmlNodeCollection nodeCollection = page.DocumentNode.SelectNodes("//div[@class='snow_depth']//p");

                foreach (HtmlNode node in nodeCollection)
                {

                }
                if (bildPath.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(bildPath.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/uploads"), guid + fileName);
                    bildPath.SaveAs(path);
                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];
                    string imagepath = newpath;
                    bilder.bildPath = imagepath;
                    WebImage img = new WebImage(bildPath.InputStream);
                    if (img.Width > 900)
                        img.Resize(900, 900);
                    img.Save(path);
                }
                db.Bilders.Add(bilder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bilder);
        }

        // GET: Bilders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bilder bilder = db.Bilders.Find(id);
            if (bilder == null)
            {
                return HttpNotFound();
            }
            return View(bilder);
        }

        // POST: Bilders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BID,bildPath,EnrollmentDate")] Bilder bilder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bilder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bilder);
        }

        // GET: Bilders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bilder bilder = db.Bilders.Find(id);
            if (bilder == null)
            {
                return HttpNotFound();
            }
            return View(bilder);
        }

        // POST: Bilders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bilder bilder = db.Bilders.Find(id);
            db.Bilders.Remove(bilder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
