namespace MalzemeTakip.Models.Domain
{
    public class Malzeme
    {
        public Guid Id { get; set; }
        public string MalzemeName { get; set; }

        public ICollection<Yemek> Yemekler { get; set; }
    }
}
