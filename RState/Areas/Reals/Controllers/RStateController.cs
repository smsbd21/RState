using System;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;

namespace RState.Areas.Reals.Controllers
{
    public class RStateController : Controller
    {
        // GET: Reals/RState
        public ActionResult Index()
        {
            if (Session["login"] != null) return View();
            else return RedirectToAction("Login","Home", new { Area = "" });
        }
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult jResult = new JsonResult();
            var oList = new List<Tb_Pictures>();
            var oFiles = Request.Files;

            for (var i = 0; i < oFiles.Count; i++)
            {
                var oPic = oFiles[i];
                var oFile = Guid.NewGuid() + Path.GetExtension(oPic.FileName);
                var oPath = Server.MapPath("~/images/site/") + oFile;
                oPic.SaveAs(oPath);

                // For Database Part
                var oDbPic = new Tb_Pictures { PicUrl = oFile };
                if (this.SavePicture(oDbPic))
                {
                    oList.Add(oDbPic);
                }
            }
            jResult.Data = oList;
            return jResult;
        }
        private bool SavePicture(Tb_Pictures oPic)
        {
            var db = new DbCon();
            db.Tb_Pictures.Add(oPic);
            return db.SaveChanges() > 0;
        }
    }
}