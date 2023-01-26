using System.ComponentModel.DataAnnotations;

namespace Hamburgercim.Models.Data.Classes
{
    public class EkstraMalzeme
    {
        public int EkstraMalzemeId { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public string EkstraMalzemeAd { get; set; } = null!;

        [Required(ErrorMessage = "{0} Alanı Boş Olamaz!")]
        public decimal EkstraMalzemeFiyat { get; set; }

        public Siparis? Siparis { get; set; }
    }
}
