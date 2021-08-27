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
    public class GrievancesController : Controller
    {
        private MyDBContext db = new MyDBContext();

        List<SelectListItem> Type = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "Complaint", Value = "Complaint"},
            new SelectListItem{ Text = "Suggestion", Value = "Suggestion"},
        };
        List<SelectListItem> Status = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "New", Value = "New"},
        };

        List<SelectListItem> UserCompletionStatus = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "Complete", Value = "Complete"},
        };
        List<SelectListItem> AdminStatus = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "In Progress", Value = "In Progress"},
            new SelectListItem{ Text = "Resolved", Value = "Resolved"},
        };

        // GET: Grievances
        public ActionResult Index()
        {
            if (Session["UserType"].Equals("Admin"))
            {
                var grievances = from g in db.Grievances where (g.Status == "New" || g.Status == "In Progress") orderby g.Created_On select g;
                return View(grievances.ToList());

            }
            else
            {
                int user_id = Convert.ToInt32(Session["UserId"]);
                User u = db.Users.FirstOrDefault(us => us.User_Id == user_id);
                int uuid = u.User_Id;
                var grivanaces = from g in db.Grievances where (g.User_Id == uuid) orderby g.Status, g.Created_On select g;
                return View("UserIndex", db.Grievances.ToList());
            }
        }


    public ActionResult List()
    {
        return View(db.Grievances.ToList());
    }



    // GET: Service/ReportService
    public ActionResult ReportGrievance()
    {

        if (Session["UserType"].Equals("Admin"))
        {
            ViewBag.Message = "History of Grievances";
            return View(db.Grievances.ToList());
        }
        else
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

    }
    // POST: Service/ReportService
    [HttpPost]
    public ActionResult ReportGrievance(DateTime fromdate, DateTime Todate)
    {
        var grievances = from a in db.Grievances where (DbFunctions.TruncateTime(a.Created_On) >= fromdate.Date && DbFunctions.TruncateTime(a.Created_On) <= Todate.Date) select a;
        if (Session["UserType"].Equals("Admin"))
        {
            ViewBag.Message = "Showing  Grievance  From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
            return View(grievances.ToList());
        }
        else
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

    }



    // GET: Grievances/Details/5
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grievance grievance = db.Grievances.Find(id);
            if (grievance == null)
            {
                return HttpNotFound();
            }
            return View(grievance);
        }

        // GET: Grievances/Create
        public ActionResult Create()
        {
            ViewBag.Type = Type;
            ViewBag.Status = Status;
            return View("CreateGrievance");
        }

        // POST: Grievances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Grivanances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGrievanceViewModel cgv)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            String status = "New";
            DateTime created_On = System.DateTime.Now;
            DateTime expected_Resolution_Date = created_On.AddDays(3);
            Grievance g = new Grievance() { User_Id = uuid, Topic = cgv.Topic, Details = cgv.Details, Sub_Topic = cgv.Sub_Topic, Type = cgv.Type, Status = status, Created_On = created_On, Expected_Resolution_Date = expected_Resolution_Date };


            // grievance.Type = "Complaint";

            if (ModelState.IsValid)
            {
                db.Grievances.Add(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateGrievance", g);
        }


        // GET: Grivanances/Resolve/5
        public ActionResult Resolve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grievance grievance = db.Grievances.Find(id);
            if (grievance == null)
            {
                return HttpNotFound();
            }
            grievance.Status = "In Progress";
            db.Entry(grievance).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Status = AdminStatus;
            return View(grievance);
        }

        // POST: Grivanances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resolve(Grievance grievance)
        {

            if (ModelState.IsValid)
            {
                grievance.Status = "Resolved";
                db.Entry(grievance).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(grievance);
        }





        // GET: Grivanances/Delete/5
        public ActionResult Complete(int? id)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grievance grievance = db.Grievances.Find(id);
            if (grievance == null)
            {
                return HttpNotFound();
            }
            return View(grievance);
        }

        // POST: Grivanances/Delete/5
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteConfirmed(int id)
        {
            Grievance grievance = db.Grievances.Find(id);
            grievance.Status = "Complete";
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: Grievances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grievance grievance = db.Grievances.Find(id);
            if (grievance == null)
            {
                return HttpNotFound();
            }
            return View(grievance);
        }

        // POST: Grievances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Grievance_Id,Type,User_Id,Topic,Sub_Topic,Details,Expected_Resolution_Date,Status,Admin_Comment,Created_On")] Grievance grievance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grievance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grievance);
        }

        // GET: Grievances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grievance grievance = db.Grievances.Find(id);
            if (grievance == null)
            {
                return HttpNotFound();
            }
            return View(grievance);
        }

        // POST: Grievances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grievance grievance = db.Grievances.Find(id);
            db.Grievances.Remove(grievance);
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
