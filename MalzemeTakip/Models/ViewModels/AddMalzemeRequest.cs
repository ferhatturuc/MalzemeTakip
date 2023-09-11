namespace MalzemeTakip.Models.ViewModels
{
    public class AddMalzemeRequest
    {
        public string? MalzemeName { get; set; }

        public int? MalzemeMiktar { get; set; }
        public Guid Id { get; set; }
    }
}
