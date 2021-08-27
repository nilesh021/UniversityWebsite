using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using UniversityWebsite.Models;

namespace UniversityWebsite.Controllers
{
    public class UsersController : Controller
    {
        private MyDBContext db = new MyDBContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            User obj = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (obj != null)
            {
                Session["UserId"] = obj.User_Id.ToString();
                Session["UserName"] = obj.First_Name.ToString() + " " +obj.Last_Name.ToString();
                Session["UserType"] = "User";

                int user_id = Convert.ToInt32(Session["UserId"]);
                int m = db.Shares.Count(s => s.Shared_To_Id == user_id && s.Seen == false);
                Session["UnreadMessages"] = m;

                return RedirectToAction("UserDashBoard");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong.");
            }
            return View();
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserId"] != null )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public PartialViewResult UnreadMessages()
        {
            int user_id = Convert.ToInt32(Session["UserId"]);
            int m = db.Shares.Count(s => s.Shared_To_Id == user_id && s.Seen == false);
            Session["UnreadMessages"] = m;
            return PartialView("_UnreadMessages");
        }


        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,First_Name,Last_Name,DOB,Gender,ContactNumber,Email,Password,A1,A2,A3")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                ViewBag.Message="Your details have been submitted successfully.Please Go Back To Login Page";
            }
            return View(user);
            
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,First_Name,Last_Name,DOB,Gender,ContactNumber,Email,Password,A1,A2,A3")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }









        public ActionResult ForgotID()
        {
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotID([Bind(Include = "ContactNumber,Email,A1,A2,A3")] User user)
        {
            User u = db.Users.FirstOrDefault(us => us.Email == user.Email && us.ContactNumber == user.ContactNumber);            
            if (u == null)
            {
                ViewBag.Message="No user exists for given email id and contact number.";
                return View(user);
            }   
            else
            {
                if ((u.A1).Equals(user.A1) && (u.A2).Equals(user.A2) && (u.A3).Equals(user.A3))
                {
                    ViewBag.Message = "Your user id is "+u.User_Id.ToString();
                    return View(u);
                }
                else
                {
                    ViewBag.Message = "Wrong answers";
                    return View(u);
                }
            }
        }






        public ActionResult ForgotPassword()
        {
            return View();
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword([Bind(Include = "User_Id,A1,A2,A3")] User user)
        {
            User u = db.Users.FirstOrDefault(us => us.User_Id == user.User_Id);
            if (u == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ((u.A1).Equals(user.A1) && (u.A2).Equals(user.A2) && (u.A3).Equals(user.A3))
            //if (u.A1 == user.A1 && u.A2 == user.A2 && u.A3 == u.A3)
            {
                return RedirectToAction("ResetPassword", new { id = u.User_Id});
            }
            ViewBag.Message="Wrong answers";   
            return View();
        }



        public ActionResult ResetPassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }



        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword([Bind(Include = "User_Id,Password,A1,A2,A3")] User user)
        {
            User u = db.Users.FirstOrDefault(us => us.User_Id == user.User_Id);
            if (u == null || user==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            u.Password = user.Password;
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Login");  
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
