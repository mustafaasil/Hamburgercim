using Hamburgercim.Models.Data.Classes;
using Hamburgercim.Models.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgercim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EkstramalzemeController : Controller
    {
        private readonly UygulamaDbContext _db;

        public EkstramalzemeController(UygulamaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var ekstraMalzemeListesi = _db.EkstraMalzemeler.ToList(); 

            return View(ekstraMalzemeListesi);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Ekle(EkstraMalzeme ekstraMalzeme)
        {
            if (ModelState.IsValid)
            {
                _db.EkstraMalzemeler.Add(ekstraMalzeme);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();
           
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekEkstraMalzeme = _db.EkstraMalzemeler.Find(id);
            TempData["Id"] = guncellenecekEkstraMalzeme!.EkstraMalzemeId;
            return View(guncellenecekEkstraMalzeme);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Guncelle(EkstraMalzeme ekstraMalzeme)
        {
            var guncellenecekEkstraMalzeme = _db.EkstraMalzemeler.Find(TempData["Id"]);
            if (ModelState.IsValid)
            {
                guncellenecekEkstraMalzeme!.EkstraMalzemeAd = ekstraMalzeme.EkstraMalzemeAd;
                guncellenecekEkstraMalzeme.EkstraMalzemeFiyat = ekstraMalzeme.EkstraMalzemeFiyat;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(ekstraMalzeme);
        }

        public IActionResult Sil(int id)
        {
            var silinecekEkstraMalzeme = _db.EkstraMalzemeler.Find(id);
            _db.EkstraMalzemeler.Remove(silinecekEkstraMalzeme!);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
