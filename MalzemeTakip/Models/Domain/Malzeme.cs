namespace MalzemeTakip.Models.Domain
{
    public class Malzeme
    {
        public Guid Id { get; set; }
        public string? MalzemeName { get; set; }
        public int? MalzemeMiktar { get; set; }

        public ICollection<Yemek>? Yemekler { get; set; }
    }
}
