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
    public class ServicesController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services;
            if (Session["UserType"].Equals("Admin"))
                return View(services.ToList());
            else
            {
                return View("UserIndex", services.ToList());
            }
        }





        // GET: Service/ReportService
        public ActionResult ReportService()
        {
            var services = from s in db.Services
                           where (DbFunctions.TruncateTime(s.Start_Date) >= System.DateTime.Today.Date)
                           orderby s.Start_Date
                           select s; 
            if (Session["UserType"].Equals("Admin"))
            {
                ViewBag.Message = "Current Ongoing Services";
                return View(services.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        // POST: Service/ReportService
        [HttpPost]
        public ActionResult ReportService(DateTime fromdate, DateTime Todate)
        {
            var services = from a in db.Services where (DbFunctions.TruncateTime(a.Start_Date) >= fromdate.Date && DbFunctions.TruncateTime(a.Start_Date) <= Todate.Date) select a;
            if (Session["UserType"].Equals("Admin"))
            {
                ViewBag.Message = "Showing Services  From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
                return View(services.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

























        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.Admins, "User_Id", "First_Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Service_Id,Service_Name,Service_Description,Required_Volunteer,Start_Date")] Service service)
        {
            service.Participated_Volunteer = 0;
            service.User_Id = Convert.ToInt32(Session["UserId"]);
            service.Start_Date = service.Start_Date.Date;
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.Admins, "User_Id", "First_Name", service.User_Id);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Admins, "User_Id", "First_Name", service.User_Id);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Service_Id,Service_Name,Service_Description,Required_Volunteer,Start_Date,Participated_Volunteer")] Service service)
        //{
        //    Service serviceObj = db.Services.FirstOrDefault(s => s.Service_Id == service.Service_Id);
        //    serviceObj.Start_Date = service.Start_Date.Date;

        //    if (serviceObj.Participated_Volunteer< service.Required_Volunteer)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(serviceObj).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    ViewBag.User_Id = new SelectList(db.Admins, "User_Id", "First_Name", service.User_Id);
        //    return View(service);
        //}




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Service_Id,User_Id,Service_Name,Service_Description,Required_Volunteer,Start_Date,Participated_Volunteer")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }
        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }






        // GET: Service/Participate/5
        public ActionResult Participate(int? id)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.FirstOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer v = db.Volunteers.Where(x => x.User_Id == uuid
                     && x.Service_Id == id
                             ).FirstOrDefault();
            if (v != null)
            {
                ViewBag.Flag = "AlreadyExist";
                return View("ParticipationDetails", v);
            }
            else
            {
                Volunteer volunteer = new Volunteer() { User_Id = uuid };

                return View(volunteer);
            }
        }

        // POST: Service/Participate/5
        [HttpPost, ActionName("Participate")]
        [ValidateAntiForgeryToken]
        public ActionResult ParticipateConfirmed(int id, [Bind(Include = "Volunteer_Id,User_Id")] Volunteer volunteer)
        {
            Service service = db.Services.Find(id);

            Volunteer v = db.Volunteers.Where(x => x.User_Id == volunteer.User_Id
                      && x.Service_Id == id
                              ).FirstOrDefault();
            if (v == null)
            {

                if (service.Participated_Volunteer < service.Required_Volunteer)
                {
                    volunteer.Service_Id = id;
                    //Volunteer volunteer = new Volunteer() { Service_Id = id, User_Id = Int16.Parse(User.Identity.GetUserId())};
                    db.Volunteers.Add(volunteer);
                    db.SaveChanges();
                    service.Participated_Volunteer += 1;
                    db.Entry(service).State = EntityState.Modified;
                    db.SaveChanges();
                    // ViewBag.Message = "Participated for service" + id + "Sucessfully";
                    return View("ParticipationDetails", volunteer);
                }
                else
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return View("ParticipationDetails", v);
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
