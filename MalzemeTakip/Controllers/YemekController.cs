using MalzemeTakip.Data;
using MalzemeTakip.Models.Domain;
using MalzemeTakip.Models.ViewModels;
using MalzemeTakip.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace MalzemeTakip.Controllers
{
    public class YemekController : Controller
    {
        private readonly IMalzemeRepository malzemeRepository;
        private readonly IYemekRepository yemekRepository;

        public YemekController(IMalzemeRepository malzemeRepository,IYemekRepository yemekRepository)
        {
            this.malzemeRepository = malzemeRepository;
            this.yemekRepository = yemekRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Malzemeleri veritabanından alın
            var malzemeler = await malzemeRepository.GetAllAsync();


            // Modeli oluşturun ve SelectListItem koleksiyonunu Malzemeler'e atayın
            var model = new AddYemekRequest
            {
                Malzemeler = malzemeler.OrderBy(x => x.MalzemeName).Select(x => new SelectListItem { Text = x.MalzemeName, Value = x.Id.ToString() })
            };

            return View(model);
        }


        // Diğer işlevler burada

        // Yemek için yeni malzeme eklemek için bir ekleme işlevi
        [HttpPost]
        public async Task<IActionResult> Add(AddYemekRequest addYemekRequest)
        {
            // Yemeği oluşturun
            var yemek = new Yemek
            {
                YemekName = addYemekRequest.YemekName
            };

            // Her malzeme için döngü oluşturarak malzemeleri ekleyin
            var selectedMalzeme= new List<Malzeme>();
            foreach (var selectedMalzemeAdi in addYemekRequest.SelectedMalzemeler)
            {
                var existingMalzeme = await malzemeRepository.GetAsync(selectedMalzemeAdi);

                if (existingMalzeme != null)
                {
                    yemek.Malzemeler.Add(existingMalzeme);
                }
            }


            yemek.Malzemeler = selectedMalzeme;
            // Yemeği veritabanına ekleyin
            await yemekRepository.AddAsync(yemek);

            return RedirectToAction("Add");
        }



        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the Malzemes
            var yemekler = await yemekRepository.GetAllAsync();

            return View(yemekler);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            var yemek = await yemekRepository.GetAsync(name);

            if (yemek != null)
            {
                var editYemekRequest = new EditYemekRequest
                {
                    Id = yemek.Id,
                    YemekName = yemek.YemekName,
                };

                return View(editYemekRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditYemekRequest editYemekRequest)
        {
            var yemek = new Yemek
            {
                Id = editYemekRequest.Id,
                YemekName = editYemekRequest.YemekName,
            };

            var updatedYemek = await yemekRepository.UpdateAsync(yemek);

            if (updatedYemek != null)
            {
                // Show success notification
            }
            else
            {
                // Show error notification
            }

            return RedirectToAction("Edit", new { name = editYemekRequest.YemekName });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AddYemekRequest editYemekRequest)
        {
            var deletedYemek = await yemekRepository.DeleteAsync(editYemekRequest.Id);

            if (deletedYemek != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { name = editYemekRequest.YemekName });
        }
    }
}




/*
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
                //Selectedmalzeme = yemek.Malzemeler.Select(x => x.Id.ToString()).ToArray()
            };

            return View(model);

        }

        // pass data to view
        return View(null);
    }
}
}
*/