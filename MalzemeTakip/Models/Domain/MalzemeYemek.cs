namespace MalzemeTakip.Models.Domain
{
    public class MalzemeYemek
    {

        // Malzeme-Yemek ilişkisi
        public int MalzemeId { get; set; }
        public string MalzemeName { get; set; }

        public virtual Malzeme? Malzeme { get; set; }

        public int YemekId { get; set; }
        public virtual Yemek? Yemek { get; set; }

        public int? Miktar { get; set; } // Malzemenin miktarı
    }
}
