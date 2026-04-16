using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            //ComboBox'taki valuemember ve displaymember mantığının aynısı 
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList() select new SelectListItem { Text = i.KATEGORIAD, Value = i.KATEGORIID.ToString() }).ToList();
            //ViewBag degerleri dışarıya açmak başka dosyalarda kullanmak için bunu yazmamız gerekiyor.
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return View();
        }
    }
}