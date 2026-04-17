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
            var kategori = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = kategori;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urunGetir = db.TBLURUNLER.Find(id);
            return View("UrunGetir", urunGetir);
        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urunGuncelle = db.TBLURUNLER.Find(p1.URUNID);
            urunGuncelle.URUNAD = p1.URUNAD;
            urunGuncelle.MARKA = p1.MARKA;
            urunGuncelle.URUNKATEGORI = p1.URUNKATEGORI;
            urunGuncelle.FIYAT = p1.FIYAT;
            urunGuncelle.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}