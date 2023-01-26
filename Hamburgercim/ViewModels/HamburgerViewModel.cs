

using System.ComponentModel.DataAnnotations;

namespace Hamburgercim.ViewModels
{
    public class HamburgerViewModel
    {
        [Required(ErrorMessage ="Hamburger Adı Alan Boş Geçilmez!!!")]
        public string HamburgerAd { get; set; } = null!;

        [Required(ErrorMessage = "Hamburher Fiyat Alan Boş Geçilmez!!!")]
        public decimal HamburgerFiyat { get; set; }

        public IFormFile? HamburgerResim { get; set; }
    }
}
