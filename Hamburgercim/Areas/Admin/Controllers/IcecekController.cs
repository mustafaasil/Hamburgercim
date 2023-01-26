using Hamburgercim.Models.Data.Classes;
using Hamburgercim.Models.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Hamburgercim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IcecekController : Controller
    {
        private readonly UygulamaDbContext _db;

        public IcecekController(UygulamaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var icecekListesi = _db.Icecekler.ToList();
            return View(icecekListesi);
        }

        public IActionResult Ekle()
        {
            return View();

        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Ekle(Icecek icecek)
        {
            if (ModelState.IsValid)
            {
                _db.Icecekler.Add(icecek);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();

        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekIcecek = _db.Icecekler.Find(id);
            TempData["Id"] = guncellenecekIcecek.IcecekId;

            return View(guncellenecekIcecek);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Guncelle(Icecek icecek)
        {
            var guncellenecekıcecek = _db.Icecekler.Find((int)TempData["Id"]!);

            if (ModelState.IsValid)
            {
                guncellenecekıcecek.IcecekAd = icecek.IcecekAd;
                guncellenecekıcecek.IcecekFiyat = icecek.IcecekFiyat;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(icecek);

        }

        public IActionResult Sil(int id)
        {
            var silinecekIcecek = _db.Icecekler.Find(id);
            _db.Icecekler.Remove(silinecekIcecek!);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
