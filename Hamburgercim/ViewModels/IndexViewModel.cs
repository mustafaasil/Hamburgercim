using Hamburgercim.Models.Data.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hamburgercim.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            HamburgerList = new List<Hamburger>();
            Siparis = new Siparis();
            IcecekList = new List<Icecek>();
        }
        public List<Hamburger> HamburgerList { get; set; }

        public List<Icecek> IcecekList { get; set; }

        public int IcecekId { get; set; }

        public Siparis Siparis { get; set; }

        public List<Boyut> BoyutList { get; set; }

        public int BoyutId { get; set; }

        public IList<SelectListItem> Ekstralar { get; set; }

        public byte HamburgerAdet { get; set; }
        public string HamburgerResim { get; set; }
    }
}
