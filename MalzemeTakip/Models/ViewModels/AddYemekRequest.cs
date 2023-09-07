using Microsoft.AspNetCore.Mvc.Rendering;

namespace MalzemeTakip.Models.ViewModels
{
    public class AddYemekRequest
    {
        public string YemekName { get; set; }


        public IEnumerable<SelectListItem> Malzeme { get; set; }
        public string[] SelectedMalzeme { get; set; } = Array.Empty<string>();
        //public int MalzemeMiktar { get; set; }
    }
}
