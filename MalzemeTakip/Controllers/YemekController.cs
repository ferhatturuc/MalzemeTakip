/*using MalzemeTakip.Models.Domain;
using MalzemeTakip.Models.ViewModels;
using MalzemeTakip.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MalzemeTakip.Controllers
{
    public class YemekController : Controller
    {
        private readonly IMalzemeRepository malzemeRepository;
        private readonly IYemekRepository yemekRepository;

        public YemekController(IMalzemeRepository malzemeRepository, IYemekRepository yemekRepository)
        {
            this.malzemeRepository = malzemeRepository;
            this.yemekRepository = yemekRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var malzeme = await malzemeRepository.GetAllAsync();

            var model = new AddYemekRequest
            {
                Malzeme = malzeme.Select(x => new SelectListItem { Text = x.MalzemeName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddYemekRequest addYemekRequest)
        {
            // Mapping AddMalzemeRequest to Malzeme domain model
            var yemek = new Yemek
            {
                YemekName = addYemekRequest.YemekName
            };


            var selectedMalzeme = new List<Malzeme>();
            foreach (var selectedMalzemeId in addYemekRequest.SelectedMalzeme)
            {
                var selectedMalzemeIdAsGuid = selectedMalzemeId;
                var existingMalzeme = await malzemeRepository.GetAsync(selectedMalzemeIdAsGuid);

                if (existingMalzeme != null)
                {
                    selectedMalzeme.Add(existingMalzeme);
                }
            }

            yemek.Malzemeler = selectedMalzeme;
            await yemekRepository.AddAsync(yemek);

            return RedirectToAction("Add");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var yemek = await yemekRepository.GetAllAsync();

            return View(yemek);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            var yemek = await yemekRepository.GetAsync(name);
            var malzemeDomainModel = await malzemeRepository.GetAllAsync();

            if (yemek != null)
            {
                // map the domain model into the view model
                var model = new EditYemekRequest
                {
                    Id = yemek.Id,
                    
                    MalzemeName = malzemeDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.MalzemeName,
                        Value = x.Id.ToString()
                    }),
                    Selectedmalzeme = yemek.Malzemeler.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);

            }

            // pass data to view
            return View(null);
        }
    }
}
*/