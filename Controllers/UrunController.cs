using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TBLURUNLER.ToList();
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 8);
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
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList() select new SelectListItem { Text = i.KATEGORIAD, Value = i.KATEGORIID.ToString() }).ToList();
            ViewBag.deger = degerler;
            return View("UrunGetir", urunGetir);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urunGuncelle = db.TBLURUNLER.Find(p.URUNID);
            urunGuncelle.URUNAD = p.URUNAD;
            urunGuncelle.MARKA = p.MARKA;
            //urunGuncelle.URUNKATEGORI = p.URUNKATEGORI;
            var kategori = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urunGuncelle.URUNKATEGORI = kategori.KATEGORIID;
            urunGuncelle.FIYAT = p.FIYAT;
            urunGuncelle.STOK = p.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}