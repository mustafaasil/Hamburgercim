namespace Hamburgercim.Models.Data.Classes
{
    public class Siparis
    {
        public int SiparisId { get; set; }
        public byte SiparisAdet { get; set; }
        public decimal ToplamTutar { get; set; }
        public List<EkstraMalzeme>? EkstraMalzeme { get; set; }
        public Icecek? Icecek { get; set; }
        public Hamburger Hamburger { get; set; } = null!;
        public Boyut Boyut { get; set; } = null!;

    }
}
