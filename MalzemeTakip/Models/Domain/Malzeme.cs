namespace MalzemeTakip.Models.Domain
{
    public class Malzeme
    {
        public int Id { get; set; }
        public string? MalzemeName { get; set; }
        public int? MalzemeMiktar { get; set; }

        public ICollection<MalzemeYemek>? MalzemeYemekler { get; set; }
    }
}
