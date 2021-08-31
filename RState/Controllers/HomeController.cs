using System;
using System.Linq;
using RState.Models;
using System.Web.Mvc;

namespace RState.Controllers
{
    public class HomeController : Controller
    {
        private DbCon db = new DbCon();
        // GET: Index
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Users = db.sp_getUserList().ToList(),
                Properties = db.sp_getPropList().ToList()
            };
            return View(model);
        }
        // GET: Blog
        public ActionResult Blog()
        {
            return View();
        }
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tb_Users u)
        {
            var oVal = db.sp_getLogin(u.Email, u.HashPwd);
            foreach (var oItem in oVal)
            {
                if (oItem.Email == u.Email && oItem.HashPwd == u.HashPwd)
                {
                    Session["ulog"] = oItem.Id;
                    Session["login"] = oItem.Name;
                    return RedirectToAction("Index");
                    //return RedirectToAction("RState");
                }
                else return HttpNotFound();
            }
            return View();            
        }
        // GET: Logout
        public ActionResult Logout()
        {
            Session.Remove("ulog");
            Session.Remove("login");
            return RedirectToAction("Index");
        }
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        // POST: Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name,CellNo,Email,City,Country,Address,Share,Role,UserPic,HashPwd")] 
        public ActionResult Register(Tb_Users oUser)
        {
            //oUser.UserPic= "images/site/" + HomeViewModel.GetRandomStr(10) + "-" + brwImg.FileName.ToString();
            if (ModelState.IsValid)
            {
                //string strPath = "images/" + HomeViewModel.GetRandomStr(10) + "-" + brwImg.FileName.ToString();
                //brwImg.SaveAs(Request.PhysicalApplicationPath + "./" + strPath);

                db.Tb_Users.Add(oUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oUser);
        }
        // GET: RState
        public ActionResult RState()
        {
            if(Session["login"]!=null) return RedirectToAction("Index");
            else return RedirectToAction("Login");
        }
        // GET: RState
        public ActionResult Booking(int PropId)
        {
            if (Session["Login"] != null)
            {
                ViewBag.PropId = new SelectList(db.sp_getPropList().Where(x => x.Id.Equals(PropId)).ToList(), "Id", "Title", PropId);
                ViewBag.UserId = new SelectList(db.Tb_Users.Where(x => x.Id.Equals(Session["ulog"])).ToList(), "Id", "Name", Session["ulog"]);
                return View();
            }
            else return RedirectToAction("Login");
        }
        // POST: Home/Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,UserId,PropId,IsAllow,InvAmt,PostedOn")] 
        public ActionResult Booking(Tb_Bookings oBook)
        {
            if (ModelState.IsValid)
            {
                oBook.PostedOn = DateTime.Now;
                db.Tb_Bookings.Add(oBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropId = new SelectList(db.sp_getPropList().Where(x => x.Id.Equals(oBook.PropId)).ToList(), "Id", "Title", oBook.PropId);
            ViewBag.UserId = new SelectList(db.Tb_Users.Where(x => x.Id.Equals(oBook.UserId)).ToList(), "Id", "Name", oBook.UserId);
            return View(oBook);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}