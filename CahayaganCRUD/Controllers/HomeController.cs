using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CahayaganCRUD.Controllers
{
    [Authorize(Roles ="User,Manager")]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            //var list = new List<user>();
            //using (var db = new dbsys32Entities())
            //{
            //    list = db.user.ToList();
            //}
            //    return View(list);
            return View(userRepo.GetAll());
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(user u)
        {
            var _user = userRepo.Table.Where(m => m.username == u.username && m.password == u.password).FirstOrDefault();

            if (_user != null)
            {
                FormsAuthentication.SetAuthCookie(u.username, false);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "User not Exist or Incorrect Password");
            return View(u);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user u)
        {
            using (var db = new dbsys32Entities())
            {
                var Newuser = new user();
                Newuser.username = u.username;
                Newuser.password = u.password;

                db.user.Add(Newuser);
                db.SaveChanges();

               TempData["msg"] = $"Added {Newuser.username} Successfully";
            }
                return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var u = new user();
            using (var db = new dbsys32Entities())
            {
                u = db.user.Find(id);
            }
                return View(u);
        }
        [HttpPost]
        public ActionResult Update(user u)
        {
            using (var db = new dbsys32Entities())
            {
                var Newuser = db.user.Find(u.id);
                Newuser.username = u.username;
                Newuser.password = u.password;

                db.user.Add(Newuser);
                db.SaveChanges();

                TempData["Msg"] = $"Updated {Newuser.username} Successfully";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            var u = new user();
            using (var db = new dbsys32Entities())
            {
                u = db.user.Find(id);
                db.user.Remove(u);
                db.SaveChanges();

                TempData["Msg"] = $"Deleted {u.username} Successfully";
                return RedirectToAction("Index");
            }


        }
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            return View(userRepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(user u)
        {
            userRepo.Update(u.id, u);
            TempData["Msg"] = $"User {u.username} updated!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(userRepo.Get(id));
        }
    }
}