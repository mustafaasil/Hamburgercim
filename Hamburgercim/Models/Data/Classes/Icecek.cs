using System.ComponentModel.DataAnnotations;

namespace Hamburgercim.Models.Data.Classes
{
    public class Icecek
    {
        public int IcecekId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string IcecekAd { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal IcecekFiyat { get; set; }
    }
}
