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
    public class EventsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        
        // GET: Events
        public ActionResult Index()
        {

            if (Session["UserType"].Equals("Admin"))
                return View(db.Events.ToList());
            else
            {
                int user_id = Convert.ToInt32(Session["UserId"]);
                User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
                int uuid = u.User_Id;
                try
                {
                    var p = from a in db.Events_User_Relations where (a.User_Id == uuid) select a;
                    var q = (from e in db.Events
                             join ea in p on e.Event_Id equals ea.Event_Id into result1

                             from a in result1.DefaultIfEmpty()
                             select new
                             {
                                 User_Id = a == null ? 0 : a.User_Id,
                                 Event_Id = e.Event_Id,
                                 Category = e.Category,
                                 Event_Name = e.Event_Name,
                                 Start_Date = e.Start_Date,
                                 Participated_User = e.Participated_User,
                                 End_Date = e.End_Date,

                                 Attendance = a == null ? 0 : a.Attendance,
                                 Comment = a == null ? null : a.Comment,
                                 Interest = a == null ? 0 : a.Interest,
                                 LikeDislike = a == null ? 0 : a.Like_Dislike

                             });

                    List<UserIndexEventViewModel> result = new List<UserIndexEventViewModel>();
                    if (q != null)
                    {
                        foreach (var a in q)
                        {
                            result.Add(new UserIndexEventViewModel()
                            {
                                User_Id = a.User_Id,

                                Event_Id = a.Event_Id,
                                Category = a.Category,
                                Event_Name = a.Event_Name,
                                Start_Date = a.Start_Date,
                                Participated_User = a.Participated_User,
                                End_Date = a.End_Date,
                                Attendance = a.Attendance,
                                Comment = a.Comment,
                                Interest = a.Interest,
                                Like_Dislike = a.LikeDislike
                            });


                        }

                        return View("_User_Index", result);
                    }
                    else
                    {
                        ViewBag.Message = "Events";
                        return View("_User_Index");

                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

            }

        }


        [HttpPost]
        public ActionResult Index(DateTime fromdate, DateTime Todate)

        {
            if (fromdate != null && Todate != null)
            {
                int user_id = Convert.ToInt32(Session["UserId"]);
                User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
                int uuid = u.User_Id;
                var p = from a in db.Events_User_Relations where (a.User_Id == uuid) select a;
                var q = (from e in db.Events
                         join ea in p on e.Event_Id equals ea.Event_Id into result1

                         from a in result1.DefaultIfEmpty()

                         where ((DbFunctions.TruncateTime(e.Start_Date) >= fromdate.Date && DbFunctions.TruncateTime(e.Start_Date) <= Todate.Date))
                         select new
                         {
                             User_Id = a == null ? 0 : a.User_Id,
                             Event_Id = e.Event_Id,
                             Category = e.Category,
                             Event_Name = e.Event_Name,
                             Start_Date = e.Start_Date,
                             Participated_User = e.Participated_User,
                             End_Date = e.End_Date,

                             Attendance = a == null ? 0 : a.Attendance,
                             Comment = a == null ? null : a.Comment,
                             Interest = a == null ? 0 : a.Interest,
                             LikeDislike = a == null ? 0 : a.Like_Dislike

                         });

                List<UserIndexEventViewModel> result = new List<UserIndexEventViewModel>();
                if (q != null)
                {
                    foreach (var a in q)
                    {
                        result.Add(new UserIndexEventViewModel()
                        {
                            User_Id = a.User_Id,

                            Event_Id = a.Event_Id,
                            Category = a.Category,
                            Event_Name = a.Event_Name,
                            Start_Date = a.Start_Date,
                            Participated_User = a.Participated_User,
                            End_Date = a.End_Date,
                            Attendance = a.Attendance,
                            Comment = a.Comment,
                            Interest = a.Interest,
                            Like_Dislike = a.LikeDislike
                        });


                    }
                    ViewBag.Message = "Showing Events Starting   From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
                    return View("_User_Index", result);
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = " No Events";
                return RedirectToAction("Index");

            }
        }



        public ActionResult Like(int id)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            Event @event = db.Events.Find(id);
            Events_User_Relation v = db.Events_User_Relations.Where(x => x.User_Id == uuid
                    && x.Event_Id == @event.Event_Id
                            ).FirstOrDefault();
            if (v != null)
            {
                v.Like_Dislike = 1;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                Events_User_Relation events_User_Relation = new Events_User_Relation() { User_Id = uuid, Event_Id = @event.Event_Id, Comment = "", Like_Dislike = 1, Interest = 0 };
                if (ModelState.IsValid)
                {
                    db.Events_User_Relations.Add(events_User_Relation);
                    db.SaveChanges();


                }

                return RedirectToAction("Index");
            }




        }
        public ActionResult Dislike(int id)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            Event @event = db.Events.Find(id);
            Events_User_Relation v = db.Events_User_Relations.Where(x => x.User_Id == uuid
                    && x.Event_Id == @event.Event_Id
                            ).FirstOrDefault();
            if (v != null)
            {
                v.Like_Dislike = -1;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                Events_User_Relation events_User_Relation = new Events_User_Relation() { User_Id = uuid, Event_Id = @event.Event_Id, Comment = "", Like_Dislike = -1, Interest = 0 };
                if (ModelState.IsValid)
                {
                    db.Events_User_Relations.Add(events_User_Relation);
                    db.SaveChanges();


                }

                return RedirectToAction("Index");
            }

        }


        public ActionResult Interest(int id)
        {

            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            Event @event = db.Events.Find(id);
            Events_User_Relation v = db.Events_User_Relations.Where(x => x.User_Id == uuid
                    && x.Event_Id == @event.Event_Id
                            ).FirstOrDefault();
            if (v != null)
            {
                v.Interest = 1;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                Events_User_Relation events_User_Relation = new Events_User_Relation() { User_Id = uuid, Event_Id = @event.Event_Id, Comment = "", Like_Dislike = 0, Interest = 1 };
                if (ModelState.IsValid)
                {
                    db.Events_User_Relations.Add(events_User_Relation);
                    db.SaveChanges();


                }

                return RedirectToAction("Index");
            }

        }
        public ActionResult Disinterest(int id)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            Event @event = db.Events.Find(id);
            Events_User_Relation v = db.Events_User_Relations.Where(x => x.User_Id == uuid
                    && x.Event_Id == @event.Event_Id
                            ).FirstOrDefault();
            if (v != null)
            {
                v.Interest = -1;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                Events_User_Relation events_User_Relation = new Events_User_Relation() { User_Id = uuid, Event_Id = @event.Event_Id, Comment = "", Like_Dislike = 0, Interest = -1 };
                if (ModelState.IsValid)
                {
                    db.Events_User_Relations.Add(events_User_Relation);
                    db.SaveChanges();


                }

                return RedirectToAction("Index");
            }

        }
        public PartialViewResult Add(int id)
        {

            AddEventReactionViewModel aerm = new AddEventReactionViewModel() { Event_Id = id };
            return PartialView("AddEventReaction", aerm);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Add(AddEventReactionViewModel aerm)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;

            Events_User_Relation v = db.Events_User_Relations.Where(x => x.User_Id == uuid
                    && x.Event_Id == aerm.Event_Id
                            ).FirstOrDefault();
            if (v != null)
            {
                v.Comment = aerm.Comment;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();

                return PartialView("DetailsReaction", v);

            }
            else
            {
                Events_User_Relation events_User_Relation = new Events_User_Relation() { User_Id = uuid, Event_Id = aerm.Event_Id, Comment = aerm.Comment, Like_Dislike = 0, Interest = 0 };
                if (ModelState.IsValid)
                {
                    db.Events_User_Relations.Add(events_User_Relation);
                    db.SaveChanges();


                }
                return PartialView("DetailsReaction");
            }

        }




        public ActionResult OtherMember(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            Event @event = db.Events.Find(id);
            var v = db.Events_User_Relations.Where(x => x.Event_Id == id
                            ).ToList();
            if (v == null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<OtherMemberViewModelcs> result = from a in v join b in db.Users on a.User_Id equals b.User_Id select new OtherMemberViewModelcs { UserName = b.First_Name + "" + b.Last_Name, EventName = a.Event.Event_Name };
            return View(result);
        }


        public ActionResult SimilarEvent(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;
            Event @event = db.Events.Find(id);
            IEnumerable<Event> v = db.Events.Where(x => x.Category == @event.Category).ToList();
            if (v == null)
            {
                ViewBag.Flag = "OOPS THERE ARE NO SIMILAR UPCOMING EVENTS";
                return View(v);
            }
            ViewBag.Flag = "SIMILAR UPCOMING EVENTS";

            return View(v);

        }






        // GET: Service/ReportService
        public ActionResult ReportEvent()
        {
           
            if (Session["UserType"].Equals("Admin"))
            {
                ViewBag.Message = "ALL Events";
                return View(db.Events.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        // POST: Service/ReportService
        [HttpPost]
        public ActionResult ReportEvent(DateTime fromdate, DateTime Todate)
        {
            var events = from a in db.Events where (DbFunctions.TruncateTime(a.Start_Date) >= fromdate.Date && DbFunctions.TruncateTime(a.Start_Date) <= Todate.Date) select a;
            if (Session["UserType"].Equals("Admin"))
            {
                ViewBag.Message = "Showing Events  From  " + fromdate.ToString("MM/dd/yyyy") + "   To  " + Todate.ToString("MM/dd/yyyy");
                return View(events.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }





        public ActionResult Participate(int? id)
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            User u = db.Users.SingleOrDefault(us => us.User_Id == user_id);
            int uuid = u.User_Id;

            Event @event = db.Events.Find(id);
            Events_User_Relation v = db.Events_User_Relations.Where(x => x.User_Id == uuid
                    && x.Event_Id == @event.Event_Id
                            ).FirstOrDefault();
            if (v != null)
            {
                v.Attendance = 1;
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Flag = "Attending";
                return RedirectToAction("Index");

            }
            else
            {
                Events_User_Relation events_User_Relation = new Events_User_Relation() { User_Id = uuid, Event_Id = @event.Event_Id, Comment = "", Like_Dislike = 0, Interest = 0, Attendance = 1 };
                if (ModelState.IsValid)
                {
                    db.Events_User_Relations.Add(events_User_Relation);
                    db.SaveChanges();
                    @event.Participated_User += 1;
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.Flag = "Attending";
                return RedirectToAction("Index");
            }
        }








        public ActionResult Share(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }




        //GET
        public PartialViewResult ShareEvent()
        {
            return PartialView("_ShareEvent");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ShareEvent([Bind(Include = "Event_Id,Email,ContactNumber")] ShareEventViewModel suvm)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == suvm.Email);
            Event eventObj = db.Events.FirstOrDefault(e => e.Event_Id == suvm.Event_Id);
            ViewBag.ElementId = eventObj.Event_Id;

            if (suvm.Email == null && suvm.ContactNumber == null)
            {
                ModelState.Clear();
                return PartialView("_ShareEvent");
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
            share.Shared_Type = "Event";
            share.Shared_Type_Id = suvm.Event_Id;
            share.Shared_Item_Title = eventObj.Event_Name;
            share.Seen = false;

            if (ModelState.IsValid)
            {
                db.Shares.Add(share);
                db.SaveChanges();
            }

            ModelState.Clear();

            return PartialView("_ShareEvent");
        }














        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }



        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_Id,Event_Name,Start_Date,End_Date,Category,Participated_User")] Event @event)
        {


            if (@event.Start_Date > System.DateTime.Now && @event.End_Date > System.DateTime.Now && @event.Start_Date < @event.End_Date)
            {
                if (ModelState.IsValid)
                {
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_Id,Event_Name,Start_Date,End_Date,Category,Participated_User")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }


        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
           
            var share = db.Shares.Where(s => s.Shared_Type.Equals("Event") && s.Shared_Type_Id == @event.Event_Id).ToList();
            foreach (Share item in share)
            {
                db.Shares.Remove(item);
                db.SaveChanges();
            }

            db.Events.Remove(@event);
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
