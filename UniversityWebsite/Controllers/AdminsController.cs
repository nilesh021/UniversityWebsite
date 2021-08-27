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
    public class AdminsController : Controller
    {
        private MyDBContext db = new MyDBContext();



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            var obj = db.Admins.FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);
            if (obj != null)
            {
                Session["UserId"] = obj.User_Id.ToString();
                Session["UserName"] = obj.First_Name.ToString() + " " + obj.Last_Name.ToString();
                Session["UserType"] = "Admin";
                return RedirectToAction("AdminDashBoard");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong.");
            }
            return View();
        }

        public ActionResult AdminDashBoard()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }














        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,First_Name,Last_Name,DOB,Gender,ContactNumber,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,First_Name,Last_Name,DOB,Gender,ContactNumber,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
        public ActionResult ForgotID([Bind(Include = "ContactNumber,Email,A1,A2,A3")] Admin admin)
        {
            Admin adminObj = db.Admins.FirstOrDefault(a => a.Email == admin.Email && a.ContactNumber == admin.ContactNumber);
            if (adminObj == null)
            {
                ViewBag.Message("No user exists for given email id and contact number.");
                return View(adminObj);
            }
            else
            {

                //if (adminObj.A1 == admin.A1 && adminObj.A2 == admin.A2 && adminObj.A3 == admin.A3)
                if ((adminObj.A1).Equals(admin.A1) && (adminObj.A2).Equals(admin.A2) && (adminObj.A3).Equals(admin.A3))
                {
                    ViewBag.Message = "Your user id is " + adminObj.User_Id.ToString();
                    return View(adminObj);
                }
                else
                {
                    ViewBag.Message = "Wrong answers";
                    return View(adminObj);
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
        public ActionResult ForgotPassword([Bind(Include = "User_Id,A1,A2,A3")] Admin admin)
        {
            Admin adminObj = db.Admins.FirstOrDefault(a => a.User_Id == admin.User_Id);
            if (adminObj == null || admin == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ((adminObj.A1).Equals(admin.A1) && (adminObj.A2).Equals(admin.A2) && (adminObj.A3).Equals(admin.A3))
              //  if (adminObj.A1 == admin.A1 && adminObj.A2 == admin.A2 && adminObj.A3 == admin.A3)
            {
                return RedirectToAction("ResetPassword", new { id = adminObj.User_Id });
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
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }



        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword([Bind(Include = "User_Id,Password,A1,A2,A3")]Admin admin)
        {
            Admin adminObj = db.Admins.FirstOrDefault(a => a.User_Id == admin.User_Id);
            if (adminObj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminObj.Password = admin.Password;
            db.Entry(adminObj).State = EntityState.Modified;
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
