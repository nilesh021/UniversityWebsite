using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityWebsite.Models;

namespace UniversityWebsite.Controllers
{
    public class HelpsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Helps
        public ActionResult Index()
        {
     
            if (Session["UserType"].Equals("Admin"))
            {
                var result = from h in db.Helps.Where(a => a.AdminResolution == "Pending") select h;


                return View(result.ToList());
            }
            else
            {
                int user_id = Convert.ToInt32(Session["UserId"]);
                User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
                int uuid = u.User_Id;

                var result = from h in db.Helps.Where(a => a.User_Id == uuid) select h;
                return View("UserIndex", result.ToList());
            }

        }

        // GET: Helps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Help help = db.Helps.Find(id);
            if (help == null)
            {
                return HttpNotFound();
            }
            return View(help);
        }

        // GET: Helps/Create
        public ActionResult Create()//user
        {
            return View("CreateHelpTicket");
        }

        // POST: Helps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateHelpTicketViewModel helpvm)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;


            Help help = new Help() {User_Id=uuid ,Email=helpvm.Email,Issue=helpvm.Issue,Description=helpvm.Description,DOT=System.DateTime.Now,AdminResolution="Pending" };

            if (ModelState.IsValid)
            {
                db.Helps.Add(help);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(help);
        }

        // GET: Helps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Help help = db.Helps.Find(id);
            if (help == null)
            {
                return HttpNotFound();
            }
            return View(help);
        }

        // POST: Helps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HelpId,User_Id,Email,Issue,Description,DOT,AdminResolution")] Help help)
        {
            if (ModelState.IsValid)
            {
                db.Entry(help).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(help);
        }

        // GET: Helps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Help help = db.Helps.Find(id);
            if (help == null)
            {
                return HttpNotFound();
            }
            return View(help);
        }

        // POST: Helps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Help help = db.Helps.Find(id);
            db.Helps.Remove(help);
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
