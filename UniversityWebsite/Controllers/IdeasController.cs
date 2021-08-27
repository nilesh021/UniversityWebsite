using System;
using System.Collections;
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
    public class IdeasController : Controller
    {
        private MyDBContext db = new MyDBContext();

       


       


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.User_Id == idea.User_Id);
            var club = db.Clubs.FirstOrDefault(c => c.Club_Id == idea.Club_Id);
            var category = db.IdeaCategories.FirstOrDefault(ic => ic.Category_Id == idea.Category_Id);
            ViewBag.UserName = user.First_Name + " " + user.Last_Name;
            ViewBag.ClubName = club.Club_Name;
            ViewBag.CategoryName = category.Name;

            return View(idea);
        }



        // GET: Ideas/Create
        public ActionResult Create()
        {
            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name");
            ViewBag.Category_Id = new SelectList(db.IdeaCategories, "Category_Id", "Name");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name");
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idea_Id,Idea_Content,Club_Id,Category_Id")] Idea idea)
        {
            if (idea != null)
            {
                idea.Votes = 0;
                idea.Posted_Date = DateTime.Today;
                idea.User_Id = Convert.ToInt32(Session["UserId"]);
                db.Ideas.Add(idea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name", idea.Club_Id);
            ViewBag.Category_Id = new SelectList(db.IdeaCategories, "Category_Id", "Name", idea.Category_Id);
            return View(idea);
        }

        // GET: Ideas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name", idea.Club_Id);
            ViewBag.Category_Id = new SelectList(db.IdeaCategories, "Category_Id", "Name", idea.Category_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", idea.User_Id);
            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idea_Id,User_Id,Idea_Content,Club_Id,Category_Id,Votes,Posted_Date")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Club_Id = new SelectList(db.Clubs, "Club_Id", "Club_Name", idea.Club_Id);
            ViewBag.Category_Id = new SelectList(db.IdeaCategories, "Category_Id", "Name", idea.Category_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", idea.User_Id);
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.User_Id == idea.User_Id);
            var club = db.Clubs.FirstOrDefault(c => c.Club_Id == idea.Club_Id);
            var category = db.IdeaCategories.FirstOrDefault(ic => ic.Category_Id == idea.Category_Id);
            ViewBag.UserName = user.First_Name + " " + user.Last_Name;
            ViewBag.ClubName = club.Club_Name;
            ViewBag.CategoryName = category.Name;
            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Idea idea = db.Ideas.Find(id);
            var idea_User_Relations = db.Idea_User_Relations.Where(iur => iur.Idea_Id == idea.Idea_Id).ToList();
            foreach(Idea_User_Relation item in idea_User_Relations)
            {
                db.Idea_User_Relations.Remove(item);
                db.SaveChanges();
            }

            var share = db.Shares.Where(s => s.Shared_Type.Equals("Idea") && s.Shared_Type_Id==idea.Idea_Id).ToList();
            foreach (Share item in share)
            {
                db.Shares.Remove(item);
                db.SaveChanges();
            }

            db.Ideas.Remove(idea);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult IdeaComments(Idea idea)
        {
            Idea ideaObj = db.Ideas.FirstOrDefault(i => i.Idea_Id == idea.Idea_Id);
            ViewBag.Votes = ideaObj.Votes;
            var idea_User_Relations = db.Idea_User_Relations.Where(i => i.Idea_Id == idea.Idea_Id && !(i.Comment.Equals(""))).Include(ide => ide.Idea).Include(u => u.User);
            return PartialView("_IdeaComments", idea_User_Relations.ToList());
        }


        //GET
        public PartialViewResult AddReaction()
        {
            ViewBag.Idea_Id = new SelectList(db.Ideas, "Idea_Id", "Idea_Content");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name");
            return PartialView("_AddReaction");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult AddReaction([Bind(Include = "Idea_Relation_Id,Idea_Id,Vote,Comment")] Idea_User_Relation idea_User_Relation)
        {
            idea_User_Relation.User_Id = Convert.ToInt32(Session["UserId"]);

            Idea idea = db.Ideas.FirstOrDefault(i => i.Idea_Id == idea_User_Relation.Idea_Id);
            idea_User_Relation.Idea_Id = idea.Idea_Id;

            if (!(idea_User_Relation.Vote == 0 && (idea_User_Relation.Comment == null || idea_User_Relation.Comment.Equals(""))))
            {
                if (ModelState.IsValid)
                {
                    db.Idea_User_Relations.Add(idea_User_Relation);
                    db.SaveChanges();
                }

                idea.Votes += idea_User_Relation.Vote;

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
            }

            ViewBag.Idea_Id = new SelectList(db.Ideas, "Idea_Id", "Idea_Content", idea_User_Relation.Idea_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "First_Name", idea_User_Relation.User_Id);
            ModelState.Clear();
            return PartialView("_AddReaction");
        }
        
        
        public ActionResult Index(string filterButton, string clearButton, string cn="",string ctn="")
        {
            if (clearButton != null)
                return RedirectToAction("Index");

            return View(db.Ideas.Include(i => i.Club).Include(i => i.IdeaCategory).Include(i => i.User).Where(i => i.Club.Club_Name.Contains(cn) && i.IdeaCategory.Name.Contains(ctn)).ToList());

        }


        //GET
        public PartialViewResult ShareIdea()
        {
            return PartialView("_ShareIdea");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ShareIdea([Bind(Include = "Idea_Id,Email,ContactNumber")] ShareIdeaViewModel suvm)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == suvm.Email);
            Idea idea = db.Ideas.FirstOrDefault(i => i.Idea_Id == suvm.Idea_Id);
            ViewBag.IdeaId = suvm.Idea_Id;

            if (suvm.Email == null && suvm.ContactNumber == null)
            {
                ModelState.Clear();
                return PartialView("_ShareIdea");
            }

            if (suvm.Email == null)
            {
                ViewBag.ErrorMessage = "Please enter email id.";
                return PartialView("_ShareError");
            }

            if (suvm.ContactNumber == null)
            {
                ViewBag.ErrorMessage = "Please give contact number.";
                return PartialView("_ShareError");
            }

            if (user == null)
            {
                ViewBag.ErrorMessage = "User Email Id does not exist.";
                return PartialView("_ShareError");
            }

            if (user.ContactNumber != suvm.ContactNumber)
            {
                ViewBag.ErrorMessage = "User Id and contact number do not match.";
                return PartialView("_ShareError");
            }

            Share share = new Share();

            share.Shared_By_Id = Convert.ToInt32(Session["UserId"]);
            share.Shared_To_Id = user.User_Id;
            share.Shared_Type = "Idea";
            share.Shared_Type_Id = suvm.Idea_Id;
            share.Shared_Item_Title = idea.Idea_Content;
            share.Seen = false;

            if (ModelState.IsValid)
            {
                db.Shares.Add(share);
                db.SaveChanges();
            }

            ModelState.Clear();

            return PartialView("_ShareIdea");
        }




        public ActionResult Feed()
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            var feed = from ic in db.IdeaCategories
                       join i in db.Ideas on ic.Category_Id equals i.Category_Id
                       join c in db.Clubs on i.Club_Id equals c.Club_Id
                       join cr in db.Club_User_Relations on c.Club_Id equals cr.Club_Id
                       join u in db.Users on i.User_Id equals u.User_Id
                       where cr.User_Id == user_id
                       orderby i.Posted_Date descending
                       select new FeedViewModel { Idea = i, Club = c, User = u, IdeaCategory = ic , Idea_Id = i.Idea_Id};
            return View(feed.ToList());
        }


       
        public ActionResult ReportIdea()
        {
            

            if (Session["UserType"].Equals("Admin"))
            {
                ViewBag.Message = " All Ideas";
                return View(db.Ideas.Include(i => i.Club).Include(i => i.IdeaCategory).Include(i => i.User).ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
       
        [HttpPost]
        public ActionResult ReportIdea(DateTime fromdate, DateTime Todate)
        {
            var ideas = from a in db.Ideas.Include(i => i.Club).Include(i => i.IdeaCategory).Include(i => i.User) where (DbFunctions.TruncateTime(a.Posted_Date) >= fromdate.Date && DbFunctions.TruncateTime(a.Posted_Date) <= Todate.Date) select a;
            if (Session["UserType"].Equals("Admin"))
            {
                ViewBag.Message = "Showing Ideas Posted   From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
                return View(ideas.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
