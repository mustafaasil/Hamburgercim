using Hamburgercim.Models.Data.Classes;
using Hamburgercim.Models.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgercim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BoyutController : Controller
    {
        private readonly UygulamaDbContext _db;

        public BoyutController(UygulamaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var boyutlarListesi = _db.Boyutlar.ToList();
            return View(boyutlarListesi);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Ekle(Boyut boyut)
        {
            if (ModelState.IsValid)
            {
                _db.Boyutlar.Add(boyut);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekBoyut = _db.Boyutlar.Find(id);
            TempData["Id"] = guncellenecekBoyut!.BoyutId;
            return View(guncellenecekBoyut);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Guncelle(Boyut boyut)
        {
            var guncellenecekBoyut = _db.Boyutlar.Find(TempData["Id"]);
            if (ModelState.IsValid)
            {
                guncellenecekBoyut!.BoyutAd = boyut.BoyutAd;
                guncellenecekBoyut.BoyutFiyat = boyut.BoyutFiyat;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(boyut);
        }

        public IActionResult Sil(int id)
        {
            var silinecekBoyut = _db.Boyutlar.Find(id);
            _db.Boyutlar.Remove(silinecekBoyut!);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
