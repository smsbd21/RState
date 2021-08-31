using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RState;
using RState.Models;

namespace RState.Areas.Reals.Controllers
{
    public class PicturesController : Controller
    {
        private DbCon db = new DbCon();

        // GET: Reals/Pictures
        public ActionResult Index()
        {
            return View(db.Tb_Pictures.ToList());
        }

        // GET: Reals/Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Pictures oPic = db.Tb_Pictures.Find(id);
            if (oPic == null)
            {
                return HttpNotFound();
            }
            return View(oPic);
        }

        // GET: Reals/Pictures/Create
        public ActionResult Create()
        {
            ViewBag.PropId = new SelectList(db.Tb_Properties, "Id", "Title");
            return View();
        }

        // POST: Reals/Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,PropId,PicUrl")] 
        public ActionResult Create(Tb_Pictures oPic, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    var oFile = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);
                    var oExt = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
                    if (oExt.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(oFile);
                        string myfile = DateTime.Now.ToString("yyyyMMdd_HHmmssFF2_") + HomeViewModel.GetRandomStr(10) + ext;
                        var oPath = Path.Combine(Server.MapPath("~/images/site/"), myfile);
                        oPic.PicUrl = myfile;
                        db.Tb_Pictures.Add(oPic);
                        db.SaveChanges();
                        file.SaveAs(oPath);
                    }
                    else ViewBag.message = "Please choose only Image file";
                }
                return RedirectToAction("Index");
            }
            ViewBag.PropId = new SelectList(db.Tb_Properties, "Id", "Title", oPic.PropId);
            return View(oPic);
        }

        // GET: Reals/Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Pictures oPic = db.Tb_Pictures.Find(id);
            if (oPic == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropId = new SelectList(db.Tb_Properties, "Id", "Title");
            return View(oPic);
        }

        // POST: Reals/Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tb_Pictures oPic, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    var oFile = Path.GetFileName(file.FileName); //getting only file name(ex-sms.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    var oExt = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
                    if (oExt.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(oFile); //getting file name without extension
                        string myfile = DateTime.Now.ToString("yyyyMMdd_HHmmssFF2_") + HomeViewModel.GetRandomStr(10) + ext; //appending the name with id
                        var oPath = Path.Combine(Server.MapPath("~/images/site/"), myfile); //store the file inside ~/project folder(images/site) 
                        oPic.PicUrl = myfile;
                        db.Entry(oPic).State = EntityState.Modified;
                        db.SaveChanges();
                        file.SaveAs(oPath);
                    }
                    else ViewBag.message = "Please choose only Image file";
                }
                return RedirectToAction("Index");
            }
            ViewBag.PropId = new SelectList(db.Tb_Properties, "Id", "Title", oPic.PropId);
            return View(oPic);
        }

        // GET: Reals/Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Pictures oPic = db.Tb_Pictures.Find(id);
            if (oPic == null)
            {
                return HttpNotFound();
            }
            return View(oPic);
        }

        // POST: Reals/Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Pictures oPic = db.Tb_Pictures.Find(id);
            db.Tb_Pictures.Remove(oPic);
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
