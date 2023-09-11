namespace MalzemeTakip.Models.Domain
{
    public class Yemek
    {
        public Guid Id { get; set; }
        public string? YemekName { get; set; }
        public string? MalzemeName { get; set; }
        public int? MalzemeMiktar { get; set; }


        public ICollection<Malzeme>? Malzemeler { get; set; }

    }
}
