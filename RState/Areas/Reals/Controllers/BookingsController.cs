using System;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace RState.Areas.Reals.Controllers
{
    public class BookingsController : Controller
    {
        private DbCon db = new DbCon();

        // GET: Reals/Bookings
        public ActionResult Index(string sUser, string sTitle)
        {
            var oBook = db.Tb_Bookings.Include(t => t.Tb_Properties).Include(t => t.Tb_Users);
            if (!string.IsNullOrEmpty(sUser)) oBook = oBook.Where(x => x.Tb_Users.Name.ToLower().Contains(sUser.ToLower()));
            if (!string.IsNullOrEmpty(sTitle)) oBook = oBook.Where(x => x.Tb_Properties.Title.ToLower().Contains(sTitle.ToLower()));
            return View(oBook.ToList());
        }

        // GET: Reals/Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Bookings oBook = db.Tb_Bookings.Find(id);
            if (oBook == null)
            {
                return HttpNotFound();
            }
            return View(oBook);
        }

        // GET: Reals/Bookings/Create
        public ActionResult Create()
        {
            ViewBag.PropId = new SelectList(db.sp_getPropList(), "Id", "Title");
            ViewBag.UserId = new SelectList(db.Tb_Users, "Id", "Name");
            return View();
        }

        // POST: Reals/Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,UserId,PropId,IsAllow,InvAmt,PostedOn")] 
        public ActionResult Create(Tb_Bookings oBook)
        {
            if (ModelState.IsValid)
            {
                oBook.PostedOn = DateTime.Now;
                db.Tb_Bookings.Add(oBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PropId = new SelectList(db.sp_getPropList(), "Id", "Title", oBook.PropId);
            ViewBag.UserId = new SelectList(db.Tb_Users, "Id", "Name", oBook.UserId);
            return View(oBook);
        }

        // GET: Reals/Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Bookings oBook = db.Tb_Bookings.Find(id);
            if (oBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropId = new SelectList(db.sp_getPropList(), "Id", "Title", oBook.PropId);
            ViewBag.UserId = new SelectList(db.Tb_Users, "Id", "Name", oBook.UserId);
            return View(oBook);
        }

        // POST: Reals/Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,UserId,PropId,IsAllow,InvAmt,PostedOn")] 
        public ActionResult Edit(Tb_Bookings oBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropId = new SelectList(db.sp_getPropList(), "Id", "Title", oBook.PropId);
            ViewBag.UserId = new SelectList(db.Tb_Users, "Id", "Name", oBook.UserId);
            return View(oBook);
        }

        // GET: Reals/Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Bookings oBook = db.Tb_Bookings.Find(id);
            if (oBook == null)
            {
                return HttpNotFound();
            }
            return View(oBook);
        }

        // POST: Reals/Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Bookings oBook = db.Tb_Bookings.Find(id);
            db.Tb_Bookings.Remove(oBook);
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
