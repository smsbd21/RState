using System;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using RState.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace RState.Areas.Reals.Controllers
{
    public class UsersController : Controller
    {
        private DbCon db = new DbCon();

        // GET: Reals/Users
        public ActionResult Index(string sName, string sEmail)
        {
            var oUser = db.Tb_Users.AsQueryable();
            if (!string.IsNullOrEmpty(sEmail)) oUser = oUser.Where(x => x.Email.Contains(sEmail));
            if (!string.IsNullOrEmpty(sName)) oUser = oUser.Where(x => x.Name.ToLower().Contains(sName.Trim().ToLower()));
            return View(oUser.ToList());
        }
        // GET: Reals/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users oUser = db.Tb_Users.Find(id);
            if (oUser == null)
            {
                return HttpNotFound();
            }
            return View(oUser);
        }

        // GET: Reals/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reals/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name,CellNo,Email,City,Country,Address,Share,Role,UserPic,HashPwd")] 
        public ActionResult CreateX(Tb_Users oUser)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Users.Add(oUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tb_Users oUser, HttpPostedFileBase hpfBase)
        {
            var ext = Path.GetExtension(hpfBase.FileName); //getting the extension(ex-.jpg)  
            var oFile = Path.GetFileName(hpfBase.FileName); //getting only file name(ex-sms.jpg)  
            var oExt = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
            if (oExt.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(oFile); //getting file name without extension
                string myfile = DateTime.Now.ToString("yyyyMMdd_HHmmssFF2_") + HomeViewModel.GetRandomStr(10) + ext; //appending the name with id
                var oPath = Path.Combine(Server.MapPath("~/images/site/"), myfile); //store the file inside ~/project folder(images/site)  
                //file.SaveAs(Server.MapPath("~/images/site/" + myfile));
                if (ModelState.IsValid)
                {
                    oUser.UserPic = myfile;
                    db.Tb_Users.Add(oUser);
                    db.SaveChanges();
                    hpfBase.SaveAs(oPath);
                    return RedirectToAction("Index");
                }
            }
            else ViewBag.message = "Please choose only Image file";
            return View(oUser);
        }

        // GET: Reals/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users oUser = db.Tb_Users.Find(id);
            if (oUser == null)
            {
                return HttpNotFound();
            }
            return View(oUser);
        }

        // POST: Reals/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name,CellNo,Email,City,Country,Address,Share,Status,Role,UserPic,HashPwd")] 
        public ActionResult Edit(Tb_Users oUser, HttpPostedFileBase hpfBase)
        {
            var ext = Path.GetExtension(hpfBase.FileName);
            var oFile = Path.GetFileName(hpfBase.FileName);
            var oExt = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
            if (oExt.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(oFile);
                string myfile = DateTime.Now.ToString("yyyyMMdd_HHmmssFF2_") + HomeViewModel.GetRandomStr(10) + ext;
                var oPath = Path.Combine(Server.MapPath("~/images/site/"), myfile);
                if (ModelState.IsValid)
                {
                    oUser.UserPic = myfile;
                    db.Entry(oUser).State = EntityState.Modified;
                    db.SaveChanges();
                    hpfBase.SaveAs(oPath);
                    return RedirectToAction("Index");
                }
            }
            else ViewBag.message = "Please choose only Image file";
            return View(oUser);
        }

        // GET: Reals/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users oUser = db.Tb_Users.Find(id);
            if (oUser == null)
            {
                return HttpNotFound();
            }
            return View(oUser);
        }

        // POST: Reals/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Users oUser = db.Tb_Users.Find(id);
            db.Tb_Users.Remove(oUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
