using System.Web.Mvc;

namespace RState.Controllers
{
    public class HomeController : Controller
    {
        private DbCon db = new DbCon();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Home
        public ActionResult Blog()
        {
            return View();
        }
        // GET: Home
        public ActionResult Contact()
        {
            return View();
        }
        // GET: Home
        public ActionResult About()
        {
            return View();
        }
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tb_Users_Info u)
        {
            var oVal = db.sp_getLogin(u.Email, u.HashPwd);
            foreach (var oItem in oVal)
            {
                if (oItem.Email == u.Email && oItem.HashPwd == u.HashPwd)
                {
                    Session["login"] = oItem.Name;
                    return RedirectToAction("RState");
                }
                else return HttpNotFound();
            }
            return View();            
        }
        // GET: Home
        public ActionResult Logout()
        {
            Session.Remove("login");
            return View();
        }
        // GET: Home
        public ActionResult RState()
        {
            if(Session["login"]!=null) return View();            
            else return RedirectToAction("Login");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}