using Hamburgercim.Models.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hamburgercim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly UygulamaDbContext _db;

        public DashboardController(UygulamaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var siparislerListesi = _db.Siparisler.Include(o => o.Hamburger).Include(o => o.Icecek).Include(o => o.Boyut).Include(o => o.EkstraMalzeme).OrderByDescending(s => s).ToList();
            return View(siparislerListesi);
        }
    }
}
