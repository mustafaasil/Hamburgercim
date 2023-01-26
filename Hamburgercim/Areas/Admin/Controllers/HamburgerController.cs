using Hamburgercim.Models.Data.Classes;
using Hamburgercim.Models.Data.Context;
using Hamburgercim.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgercim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HamburgerController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly Hamburger _hamburger;

        public HamburgerController(UygulamaDbContext db, Hamburger hamburger)
        {
            _db = db;
            _hamburger = hamburger;
        }

        public IActionResult Index()
        {
            var hamburgerListele = _db.Hamburgerler.ToList();

            return View(hamburgerListele);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Ekle(HamburgerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (vm.HamburgerFiyat<= 0)
            {
                return View();
            }


            if (vm.HamburgerResim != null)
            {
                //Önce resmin uzantısını çekelim
                //var uzanti = Path.GetExtension(hamburgerViewModel.HamburgerResmi.FileName);

                //Daha sonra dosyamızın adını oluşturalım 
                var dosyaAdi = vm.HamburgerResim.FileName;

                //Sonra dosyanın kaydedileceği konum belirlenir
                var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                //Sonra dosya için bir akış ortamı oluşturulur. Kaydetmek için ortam hazırlıyoruz.
                var akisOrtami = new FileStream(konum, FileMode.Create);

                //Resmi o klasöre kaydet 
                vm.HamburgerResim.CopyTo(akisOrtami);
                akisOrtami.Close();

                //Resmi o klasöre kaydettiniz
                //_db'ye de sadece dosya adını ekle
                _hamburger.HamburgerFoto = dosyaAdi;
            }

            _hamburger.HamburgerAd = vm.HamburgerAd;
            _hamburger.HamburgerFiyat = vm.HamburgerFiyat;

            if (_db.Hamburgerler.FirstOrDefault(h => h.HamburgerAd == _hamburger.HamburgerAd) != null)
            {
                return View();
            }

            _db.Hamburgerler.Add(_hamburger);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekHamburger = _db.Hamburgerler.Find(id);
            HamburgerViewModel viewModel = new HamburgerViewModel();
            viewModel.HamburgerAd = guncellenecekHamburger.HamburgerAd;
            viewModel.HamburgerFiyat = guncellenecekHamburger.HamburgerFiyat;
            TempData["Id"] = guncellenecekHamburger.HamburgerId;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Guncelle(HamburgerViewModel viewModel)
        {
            var guncellenecekHamburger = _db.Hamburgerler.Find((int)TempData["Id"]!);

            if (viewModel.HamburgerResim != null)
            {
                var dosyaAdi = viewModel.HamburgerResim.FileName;
                if (guncellenecekHamburger!.HamburgerFoto == dosyaAdi)
                {
                    return RedirectToAction(nameof(Index));
                }

                //Sonra dosyanın kaydedileceği konum belirlenir
                var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                //Sonra dosya için bir akış ortamı oluşturulur. Kaydetmek için ortam hazırlıyoruz.
                var akisOrtami = new FileStream(konum, FileMode.Create);

                //Resmi o klasöre kaydet 
                viewModel.HamburgerResim.CopyTo(akisOrtami);
                akisOrtami.Close();

                //Resmi o klasöre kaydettiniz
                //_db'ye de sadece dosya adını ekle
                guncellenecekHamburger.HamburgerFoto = dosyaAdi;
            }

            guncellenecekHamburger!.HamburgerAd = viewModel.HamburgerAd;
            guncellenecekHamburger.HamburgerFiyat = viewModel.HamburgerFiyat;

            var digerHamburgerler = _db.Hamburgerler.Except(_db.Hamburgerler.Where(u => u.HamburgerId == guncellenecekHamburger.HamburgerId)).ToList();

            if (digerHamburgerler.Any(u => u.HamburgerAd == guncellenecekHamburger.HamburgerAd))
            {
                return RedirectToAction(nameof(Index));
            }
            _db.Hamburgerler.Update(guncellenecekHamburger);
            _db.SaveChanges();



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sil(int id)
        {
            var silinecekHamburger = _db.Hamburgerler.Find(id);

            if (silinecekHamburger == null)
            {
                return View();
            }

            _db.Hamburgerler.Remove(silinecekHamburger);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Resimkaldir(int id)
        {
            var resmiSilinecekHamburger = _db.Hamburgerler.Find(id)!;
            if (resmiSilinecekHamburger.HamburgerFoto == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var resimAdi = resmiSilinecekHamburger.HamburgerFoto;
            resmiSilinecekHamburger.HamburgerFoto = null;
            _db.Hamburgerler.Update(resmiSilinecekHamburger);
            _db.SaveChanges();

            //Bu resmi kullanan başka hamburger yoksa resmi de sil.
            var silinecekHamburgerlerHaricindekiHamburgerler = _db.Hamburgerler.Except(_db.Hamburgerler.Where(u => u.HamburgerId == resmiSilinecekHamburger.HamburgerId));
            var resmiKullananBaskaYok = silinecekHamburgerlerHaricindekiHamburgerler.All(u => u.HamburgerFoto != resimAdi);

            if (resmiKullananBaskaYok)
            {
                //resimler klasörünün olduğu yerdeki dosyaları al.
                string[] dosyalar = Directory.GetFiles("D:\\ÇALIŞMALAR\\Hamburgercim\\Hamburgercim\\wwwroot\\images");
            

                //Bu dosyalardan silmek istediğimizi bulup sileceğiz.
                foreach (var item in dosyalar)
                {
                    var resimIsmiDizisi = item.Split("\\");

                    //Artık dosya adı bu resim dizisinin son elemanı oldu
                    if (resimIsmiDizisi[resimIsmiDizisi.Length - 1] == resimAdi)
                    {
                        System.IO.File.Delete(item);
                        break;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
