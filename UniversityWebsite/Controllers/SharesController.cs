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
    public class SharesController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Shares
        public ActionResult Index()
        {
            var shares = db.Shares.Include(s => s.Receiver).Include(s => s.Sender);
            return View(shares.ToList());
        }

        // GET: Shares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Share share = db.Shares.Find(id);
            if (share == null)
            {
                return HttpNotFound();
            }
            return View(share);
        }

        // GET: Shares/Create
        public ActionResult Create()
        {
            ViewBag.Shared_To_Id = new SelectList(db.Users, "User_Id", "First_Name");
            ViewBag.Shared_By_Id = new SelectList(db.Users, "User_Id", "First_Name");
            return View();
        }

        // POST: Shares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Share_Id,Shared_To_Id,Shared_Type,Shared_Type_Id,Shared_Item_Title,Shared_By_Id,Seen")] Share share)
        {
            if (ModelState.IsValid)
            {
                db.Shares.Add(share);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Shared_To_Id = new SelectList(db.Users, "User_Id", "First_Name", share.Shared_To_Id);
            ViewBag.Shared_By_Id = new SelectList(db.Users, "User_Id", "First_Name", share.Shared_By_Id);
            return View(share);
        }

        // GET: Shares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Share share = db.Shares.Find(id);
            if (share == null)
            {
                return HttpNotFound();
            }
            ViewBag.Shared_To_Id = new SelectList(db.Users, "User_Id", "First_Name", share.Shared_To_Id);
            ViewBag.Shared_By_Id = new SelectList(db.Users, "User_Id", "First_Name", share.Shared_By_Id);
            return View(share);
        }

        // POST: Shares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Share_Id,Shared_To_Id,Shared_Type,Shared_Type_Id,Shared_Item_Title,Shared_By_Id,Seen")] Share share)
        {
            if (ModelState.IsValid)
            {
                db.Entry(share).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Shared_To_Id = new SelectList(db.Users, "User_Id", "First_Name", share.Shared_To_Id);
            ViewBag.Shared_By_Id = new SelectList(db.Users, "User_Id", "First_Name", share.Shared_By_Id);
            return View(share);
        }

        // GET: Shares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Share share = db.Shares.Find(id);
            if (share == null)
            {
                return HttpNotFound();
            }
            return View(share);
        }

        // POST: Shares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Share share = db.Shares.Find(id);
            db.Shares.Remove(share);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult See(int share_id, int item_id, string type_id)
        {
            Share share = db.Shares.FirstOrDefault(s => s.Share_Id == share_id);
            share.Seen = true;

            if (ModelState.IsValid)
            {
                db.Entry(share).State = EntityState.Modified;
                db.SaveChanges();
            }

            int user_id = Convert.ToInt32(Session["UserId"]);
            int m = db.Shares.Count(s => s.Shared_To_Id == user_id && s.Seen == false);
            Session["UnreadMessages"] = m;

            if (type_id.Equals("Event"))
                return RedirectToAction("Details", "Events", new { id = item_id });
            
            if (type_id.Equals("Idea"))
                return RedirectToAction("Details", "Ideas", new { id = item_id });

            return View("SharedWithMe");
        }

        public ActionResult SharedWithMe()
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            var shares = db.Shares.Where(i => i.Shared_To_Id == user_id).OrderByDescending(i => i.Share_Id).Include(s => s.Receiver).Include(s => s.Sender);
            return View(shares.ToList());
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
