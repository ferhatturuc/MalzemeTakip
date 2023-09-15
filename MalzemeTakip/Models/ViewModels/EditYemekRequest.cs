using MalzemeTakip.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MalzemeTakip.Models.ViewModels
{
    public class EditYemekRequest
    {
        //public List<Malzeme?> Malzemeler;

        public int Id { get; set; }
        public string? YemekName { get; set; }
        public int? MalzemeId { get; set; }
        public IEnumerable<MalzemeYemek>? MalzemeYemekler { get; set; }
        public string[]? SelectedMalzemeYemekler { get; set; } = Array.Empty<string>();

    }
}
