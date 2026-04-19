using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        //db'ye erişmek için Entity kullanarak buradan tablolarımıza erişiyoruz ardından da tablolarımızdaki verileri kullanıp listeleyebiliriz.
        MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATEGORILER.ToList();
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategoriBul = db.TBLKATEGORILER.Find(id);
            //Kategori bilgilerinin getirileceği view'ı döndürüyoruz ve seçtiğimiz kategorinin bilgilerini getirmek için üstteki değişkeni de parantez içine yazıyoruz.
            return View("KategoriGetir", kategoriBul);
        }

        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var kategoriGuncelle = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            kategoriGuncelle.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}