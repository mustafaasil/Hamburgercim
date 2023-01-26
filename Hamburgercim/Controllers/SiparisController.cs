using Hamburgercim.Models.Data.Classes;
using Hamburgercim.Models.Data.Context;
using Hamburgercim.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hamburgercim.Controllers
{
    public class SiparisController : Controller
    {
        private readonly UygulamaDbContext _db;

        public SiparisController(UygulamaDbContext db)  // db bir kere newlenir
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.HamburgerList = _db.Hamburgerler.ToList();
            model.IcecekList = _db.Icecekler.ToList();
            model.Ekstralar = _db.EkstraMalzemeler.Select(e => new SelectListItem()
            {
                Text = e.EkstraMalzemeAd + "-" + e.EkstraMalzemeFiyat,
                Value = e.EkstraMalzemeId.ToString(),
            }).ToList();
            model.BoyutList = _db.Boyutlar.ToList();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel vm)
        {
            Siparis siparis = new Siparis();
            List<EkstraMalzeme> ekstraList = new List<EkstraMalzeme>();
            var secilenHamburger = _db.Hamburgerler.Where(m => m.HamburgerId == vm.Siparis.Hamburger.HamburgerId).FirstOrDefault();
            var secilenIcecek = _db.Icecekler.Where(m => m.IcecekId == vm.Siparis.Icecek.IcecekId).FirstOrDefault();
            foreach (var item in vm.Ekstralar)
            {
                if (item.Selected)
                {
                    ekstraList.Add(_db.EkstraMalzemeler.FirstOrDefault(e => e.EkstraMalzemeId == Convert.ToInt32(item.Value)));
                }
            }
            siparis.SiparisAdet = vm.HamburgerAdet;
            siparis.Hamburger = secilenHamburger;
            siparis.EkstraMalzeme = ekstraList;
            siparis.Boyut = _db.Boyutlar.Find(vm.BoyutId);
            siparis.Icecek = secilenIcecek;
            siparis.Hamburger.HamburgerFoto = secilenHamburger.HamburgerFoto;
            siparis.ToplamTutar = (siparis.Hamburger.HamburgerFiyat + siparis.Boyut.BoyutFiyat + siparis.Icecek.IcecekFiyat) * siparis.SiparisAdet;
            siparis.ToplamTutar += ekstraList.Sum(e => e.EkstraMalzemeFiyat);
            _db.Siparisler.Add(siparis);
            _db.SaveChanges();
            return RedirectToAction("SiparisGoster");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SiparisEkle(Siparis siparis)
        {
            return View();
        }

        public IActionResult SiparisGoster()
        {
            var lastOrder = _db.Siparisler
    .Include(o => o.Hamburger)
    .Include(o => o.Icecek)
    .Include(o => o.Boyut)
    .Include(o => o.EkstraMalzeme)
    .OrderByDescending(o => o.SiparisId)
    .FirstOrDefault();
            return View(lastOrder);
        }
    }
}
