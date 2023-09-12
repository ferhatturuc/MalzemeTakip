using Microsoft.AspNetCore.Mvc.Rendering;

namespace MalzemeTakip.Models.ViewModels
{
    public class EditYemekRequest
    {
        public int Id { get; set; }
        public string? YemekName { get; set; }
        public IEnumerable<SelectListItem>? Malzemeler { get; set; }
        public string[]? SelectedMalzemeler { get; set; } = Array.Empty<string>();

    }
}
