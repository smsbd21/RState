using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace RState.Controllers
{
    public class UsersInfoController : Controller
    {
        private DbCon db = new DbCon();

        // GET: UsersInfo
        public ActionResult Index()
        {
            return View(db.Tb_Users_Info.ToList());
        }

        // GET: UsersInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users_Info tb_Users_Info = db.Tb_Users_Info.Find(id);
            if (tb_Users_Info == null)
            {
                return HttpNotFound();
            }
            return View(tb_Users_Info);
        }        

        // GET: UsersInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CellNo,Email,City,Country,Address,Share,Status,Role,UserId,HashPwd")] Tb_Users_Info tb_Users_Info)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Users_Info.Add(tb_Users_Info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Users_Info);
        }

        // GET: UsersInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users_Info tb_Users_Info = db.Tb_Users_Info.Find(id);
            if (tb_Users_Info == null)
            {
                return HttpNotFound();
            }
            return View(tb_Users_Info);
        }

        // POST: UsersInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CellNo,Email,City,Country,Address,Share,Status,Role,UserId,HashPwd")] Tb_Users_Info tb_Users_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Users_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Users_Info);
        }

        // GET: UsersInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users_Info tb_Users_Info = db.Tb_Users_Info.Find(id);
            if (tb_Users_Info == null)
            {
                return HttpNotFound();
            }
            return View(tb_Users_Info);
        }

        // POST: UsersInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Users_Info tb_Users_Info = db.Tb_Users_Info.Find(id);
            db.Tb_Users_Info.Remove(tb_Users_Info);
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
