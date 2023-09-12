namespace MalzemeTakip.Models.Domain
{
    public class Yemek
    {
        public int Id { get; set; }
        public string? YemekName { get; set; }


        public ICollection<MalzemeYemek>? MalzemeYemekler { get; set; }

    }
}
