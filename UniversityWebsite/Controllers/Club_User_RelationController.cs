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
    public class Club_User_RelationController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Club_User_Relation
        public ActionResult Index()
        {
            var club_User_Relations = db.Club_User_Relations.Include(c => c.Club).Include(c => c.User);
            return View(club_User_Relations.ToList());
        }

        // GET: Club_User_Relation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_User_Relation club_User_Relation = db.Club_User_Relations.Find(id);
            if (club_User_Relation == null)
            {
                return HttpNotFound();
            }
            return View(club_User_Relation);
        }

        // GET: Club_User_Relation/Create
        public ActionResult Create()
        {
            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name");
            return View();
        }

        // POST: Club_User_Relation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Relation_Id,Club_Id,User_Id,Designation,Joined_Date")] Club_User_Relation club_User_Relation)
        {
            if (ModelState.IsValid)
            {
                db.Club_User_Relations.Add(club_User_Relation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name", club_User_Relation.Club_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", club_User_Relation.User_Id);
            return View(club_User_Relation);
        }

        // GET: Club_User_Relation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_User_Relation club_User_Relation = db.Club_User_Relations.Find(id);
            if (club_User_Relation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name", club_User_Relation.Club_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", club_User_Relation.User_Id);
            return View(club_User_Relation);
        }

        // POST: Club_User_Relation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Relation_Id,Club_Id,User_Id,Designation,Joined_Date")] Club_User_Relation club_User_Relation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(club_User_Relation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name", club_User_Relation.Club_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", club_User_Relation.User_Id);
            return View(club_User_Relation);
        }

        // GET: Club_User_Relation/Delete/5
        public ActionResult Delete(int? id, int? cid)
        {
            if (id == null|| cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_User_Relation club_User_Relation = db.Club_User_Relations.FirstOrDefault(c => c.Club_Id == cid && c.User_Id == id);
            if (club_User_Relation == null)
            {
                return HttpNotFound();
            }
            return View(club_User_Relation);
        }

        // POST: Club_User_Relation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club_User_Relation club_User_Relation = db.Club_User_Relations.FirstOrDefault(c => c.Relation_Id == id);
            db.Club_User_Relations.Remove(club_User_Relation);
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
