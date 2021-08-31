using System.Net;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace RState.Areas.Reals.Controllers
{
    public class PropertiesController : Controller
    {
        private DbCon db = new DbCon();

        // GET: Reals/Properties
        public ActionResult Index(string sTitle)
        {
            var oProp = db.Tb_Properties.AsQueryable();
            if (!string.IsNullOrEmpty(sTitle)) oProp = oProp.Where(x => x.Title.ToLower().Contains(sTitle.Trim().ToLower()));
            return View(oProp.ToList());
        }

        // GET: Reals/Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Properties oProp = db.Tb_Properties.Find(id);
            if (oProp == null)
            {
                return HttpNotFound();
            }
            return View(oProp);
        }

        // GET: Reals/Properties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reals/Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Title,Location,Price,NoShare,ExpDate,Notes")] 
        public ActionResult Create(Tb_Properties oProp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Properties.Add(oProp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oProp);
        }

        // GET: Reals/Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Properties oProp = db.Tb_Properties.Find(id);
            if (oProp == null)
            {
                return HttpNotFound();
            }
            return View(oProp);
        }

        // POST: Reals/Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Title,Location,Price,NoShare,ExpDate,Notes")] 
        public ActionResult Edit(Tb_Properties oProp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oProp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oProp);
        }

        // GET: Reals/Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Properties oProp = db.Tb_Properties.Find(id);
            if (oProp == null)
            {
                return HttpNotFound();
            }
            return View(oProp);
        }

        // POST: Reals/Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Properties oProp = db.Tb_Properties.Find(id);
            db.Tb_Properties.Remove(oProp);
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
