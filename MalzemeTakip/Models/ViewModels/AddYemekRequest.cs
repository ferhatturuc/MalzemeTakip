using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MalzemeTakip.Models.ViewModels
{
    public class AddYemekRequest
    {
        public int Id { get; set; }
        public string? YemekName { get; set; }
        public string? MalzemeName { get; set; }
        public int? MalzemeMiktar { get; set; }


        //public DateTime Date { get; set; }
        public IEnumerable<SelectListItem>? MalzemeYemekler { get; set; }
        public string[]? SelectedMalzemeler { get; set; } = Array.Empty<string>();

    }
}
