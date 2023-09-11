namespace MalzemeTakip.Models.ViewModels
{
    public class EditYemekRequest
    {
        public Guid Id { get; set; }
        public string YemekName { get; set; }
        public List<AddMalzemeRequest> Malzemeler { get; set; } = new List<AddMalzemeRequest>();

    }
}
