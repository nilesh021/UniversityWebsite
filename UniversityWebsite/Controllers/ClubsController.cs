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
    public class ClubsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        public Club Club { get; private set; }
        public Club_User_Relation Club_User_Relation { get; private set; }

        // GET: Clubs
        public ActionResult Index()
        {
            return View(db.Clubs.ToList());
        }


        // GET: Clubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Club_Id,Club_Name,Department,Max_Member")] Club club)
        {
            if (ModelState.IsValid)
            {
                db.Clubs.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(club);
        }

        // GET: Clubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Club_Id,Club_Name,Department,Max_Member")] Club club)
        {
            int m = db.Club_User_Relations.Count(r => r.Club_Id == club.Club_Id);
            if(m < club.Max_Member)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(club).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }      
            return View(club);
        }


        

        // GET: Clubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Clubs.Find(id);
            db.Clubs.Remove(club);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteMember(int? id, int? cid)
        {
            if (id == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_User_Relation club_User_Relation = db.Club_User_Relations.FirstOrDefault(r => r.Club_Id == cid && r.User_Id == id);
            if (club_User_Relation == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Club_User_Relations.Remove(club_User_Relation);
            db.SaveChanges();
            return RedirectToAction("Details",new { id = cid });
        }


        public ActionResult Details(string joinButton,string leaveButton,int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }

            int user_id = Convert.ToInt32(Session["UserId"]);
            Club_User_Relation club_User_RelationObj = db.Club_User_Relations.FirstOrDefault(i => i.Club_Id == club.Club_Id && i.User_Id == user_id);

            if (club_User_RelationObj == null)
            {
                ViewBag.Member = "No";
            }
            else
            {
                ViewBag.Member = "Yes";
                ViewBag.Designation = club_User_RelationObj.Designation;
                ViewBag.JoinedOn = club_User_RelationObj.Joined_Date.ToShortDateString();
            }
            

            if (joinButton != null)
            {
                Club_User_Relation club_User_Relation = new Club_User_Relation();
                club_User_Relation.Club_Id = club.Club_Id;
                int clubsJoined = db.Club_User_Relations.Count(r => r.User_Id == user_id);
                if(clubsJoined <4)
                {
                        club_User_Relation.User_Id = user_id;
                        
                        club_User_Relation.Joined_Date = DateTime.Today;
                        club_User_Relation.Designation = "Member";

                        int m = db.Club_User_Relations.Count(c => c.Club_Id == club.Club_Id);
                        if (m < club.Max_Member)
                        {
                            if (ModelState.IsValid)
                            {
                                db.Club_User_Relations.Add(club_User_Relation);
                                db.SaveChanges();
                            }
                        }

                } 
                return RedirectToAction("Details", new { id = club_User_Relation.Club_Id });
            }
            if (leaveButton != null)
            {
                Club_User_Relation club_User_Relation = db.Club_User_Relations.FirstOrDefault(i => i.Club_Id == id && i.User_Id == user_id);
                db.Club_User_Relations.Remove(club_User_Relation);
                db.SaveChanges();
                ViewBag.Member = "No";
            }

            ViewBag.CurrentMembers = db.Club_User_Relations.Count(i => i.Club_Id == club.Club_Id);
            return View(club);
        }



        public PartialViewResult Members(Club club)
        {
            var members = db.Club_User_Relations.Include(i => i.User).Where(c => c.Club_Id == club.Club_Id);
            return PartialView("_Members", members.ToList());
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
