using MalzemeTakip.Models.Domain;
using MalzemeTakip.Models.ViewModels;
using MalzemeTakip.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MalzemeTakip.Controllers
{
    public class MalzemeController : Controller
    {
        private readonly IMalzemeRepository malzemeRepository;

        public MalzemeController(IMalzemeRepository malzemeRepository)
        {
            this.malzemeRepository = malzemeRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddMalzemeRequest addMalzemeRequest)
        {
            // Mapping AddMalzemeRequest to Malzeme domain model
            var malzeme = new Malzeme
         
            {
                MalzemeName = addMalzemeRequest.MalzemeName
            };

            await malzemeRepository.AddAsync(malzeme);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the Malzemes
            var malzemeler = await malzemeRepository.GetAllAsync();

            return View(malzemeler);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            var malzeme = await malzemeRepository.GetAsync(name);

            if (malzeme != null)
            {
                var editMalzemeRequest = new EditMalzemeRequest
                {
                    Id = malzeme.Id,
                    MalzemeName = malzeme.MalzemeName,
                };

                return View(editMalzemeRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMalzemeRequest editMalzemeRequest)
        {
            var malzeme = new Malzeme
            {
                Id = editMalzemeRequest.Id,
                MalzemeName = editMalzemeRequest.MalzemeName,
            };

            var updatedMalzeme = await malzemeRepository.UpdateAsync(malzeme);

            if (updatedMalzeme != null)
            {
                // Show success notification
            }
            else
            {
                // Show error notification
            }

            return RedirectToAction("Edit", new { name = editMalzemeRequest.MalzemeName });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AddMalzemeRequest editMalzemeRequest)
        {
            var deletedMalzeme = await malzemeRepository.DeleteAsync(editMalzemeRequest.Id);

            if (deletedMalzeme != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { name = editMalzemeRequest.MalzemeName });
        }
 
    }
}