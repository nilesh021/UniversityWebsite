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
    public class Events_User_RelationController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Events_User_Relation
        public ActionResult Index()
        {
            var events_User_Relations = db.Events_User_Relations.Include(e => e.Event);
            return View(events_User_Relations.ToList());
        }

        // GET: Events_User_Relation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events_User_Relation events_User_Relation = db.Events_User_Relations.Find(id);
            if (events_User_Relation == null)
            {
                return HttpNotFound();
            }
            return View(events_User_Relation);
        }

        // GET: Events_User_Relation/Create
        public ActionResult Create()
        {
            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name");
            return View();
        }

        // POST: Events_User_Relation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Event_Id,User_Id,Like_Dislike,Interest,Comment,Attendance")] Events_User_Relation events_User_Relation)
        {
            if (ModelState.IsValid)
            {
                db.Events_User_Relations.Add(events_User_Relation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name", events_User_Relation.Event_Id);
            return View(events_User_Relation);
        }

        // GET: Events_User_Relation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events_User_Relation events_User_Relation = db.Events_User_Relations.Find(id);
            if (events_User_Relation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name", events_User_Relation.Event_Id);
            return View(events_User_Relation);
        }

        // POST: Events_User_Relation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Event_Id,User_Id,Like_Dislike,Interest,Comment,Attendance")] Events_User_Relation events_User_Relation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events_User_Relation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name", events_User_Relation.Event_Id);
            return View(events_User_Relation);
        }

        // GET: Events_User_Relation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events_User_Relation events_User_Relation = db.Events_User_Relations.Find(id);
            if (events_User_Relation == null)
            {
                return HttpNotFound();
            }
            return View(events_User_Relation);
        }

        // POST: Events_User_Relation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events_User_Relation events_User_Relation = db.Events_User_Relations.Find(id);
            db.Events_User_Relations.Remove(events_User_Relation);
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
