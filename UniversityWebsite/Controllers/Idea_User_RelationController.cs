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
    public class Idea_User_RelationController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Idea_User_Relation
        public ActionResult Index()
        {
            var idea_User_Relations = db.Idea_User_Relations.Include(i => i.Idea).Include(i => i.User);
            return View(idea_User_Relations.ToList());
        }

        // GET: Idea_User_Relation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea_User_Relation idea_User_Relation = db.Idea_User_Relations.Find(id);
            if (idea_User_Relation == null)
            {
                return HttpNotFound();
            }
            return View(idea_User_Relation);
        }

        // GET: Idea_User_Relation/Create
        public ActionResult Create()
        {
            ViewBag.Idea_Id = new SelectList(db.Ideas, "Idea_Id", "Idea_Content");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name");
            return View();
        }

        // POST: Idea_User_Relation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idea_Relation_Id,Idea_Id,User_Id,Vote,Comment")] Idea_User_Relation idea_User_Relation)
        {
            if (ModelState.IsValid)
            {
                db.Idea_User_Relations.Add(idea_User_Relation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idea_Id = new SelectList(db.Ideas, "Idea_Id", "Idea_Content", idea_User_Relation.Idea_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", idea_User_Relation.User_Id);
            return View(idea_User_Relation);
        }

        // GET: Idea_User_Relation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea_User_Relation idea_User_Relation = db.Idea_User_Relations.Find(id);
            if (idea_User_Relation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idea_Id = new SelectList(db.Ideas, "Idea_Id", "Idea_Content", idea_User_Relation.Idea_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", idea_User_Relation.User_Id);
            return View(idea_User_Relation);
        }

        // POST: Idea_User_Relation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idea_Relation_Id,Idea_Id,User_Id,Vote,Comment")] Idea_User_Relation idea_User_Relation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idea_User_Relation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idea_Id = new SelectList(db.Ideas, "Idea_Id", "Idea_Content", idea_User_Relation.Idea_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", idea_User_Relation.User_Id);
            return View(idea_User_Relation);
        }

        // GET: Idea_User_Relation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea_User_Relation idea_User_Relation = db.Idea_User_Relations.Find(id);
            if (idea_User_Relation == null)
            {
                return HttpNotFound();
            }
            return View(idea_User_Relation);
        }

        // POST: Idea_User_Relation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Idea_User_Relation idea_User_Relation = db.Idea_User_Relations.Find(id);
            db.Idea_User_Relations.Remove(idea_User_Relation);
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
