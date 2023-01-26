using System.ComponentModel.DataAnnotations;

namespace Hamburgercim.Models.Data.Classes
{
    public class Boyut
    {
        public int BoyutId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string BoyutAd { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal BoyutFiyat { get; set; }

    }
}
