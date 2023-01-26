using System.ComponentModel.DataAnnotations;

namespace Hamburgercim.Models.Data.Classes
{
    public class Hamburger
    {
        public int HamburgerId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string HamburgerAd { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal HamburgerFiyat { get; set; }
        public string? HamburgerFoto { get; set; }
    }
}
