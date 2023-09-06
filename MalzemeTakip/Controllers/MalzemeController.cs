using MalzemeTakip.Data;
using MalzemeTakip.Models.Domain;
using MalzemeTakip.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MalzemeTakip.Controllers
{
    public class MalzemeController : Controller
    {
        private readonly MalzemeTakipDbContext malzemeTakipDbContext;

        public MalzemeController(MalzemeTakipDbContext malzemeTakipDbContext)
        {
            this.malzemeTakipDbContext = malzemeTakipDbContext;
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

            await malzemeTakipDbContext.Malzemeler.AddAsync(malzeme);
            await malzemeTakipDbContext.SaveChangesAsync();

            return RedirectToAction("List");
            //return View("Add");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var malzemeler = await malzemeTakipDbContext.Malzemeler.ToListAsync();

            return View(malzemeler);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            //var malzeme =malzemeTakipDbContext.Malzemeler.Find(malzemeName);

            var malzeme = await malzemeTakipDbContext.Malzemeler.FirstOrDefaultAsync(m => m.MalzemeName == name);

            if (malzeme != null)
            {
                var editMalzemeRequest = new EditMalzemeRequest
                {
                    Id = malzeme.Id,
                    MalzemeName = malzeme.MalzemeName
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
                MalzemeName = editMalzemeRequest.MalzemeName
            };

            var existingMalzeme = await malzemeTakipDbContext.Malzemeler.FindAsync(malzeme.Id);

            if (existingMalzeme != null)
            {
                existingMalzeme.MalzemeName = malzeme.MalzemeName;

                await malzemeTakipDbContext.SaveChangesAsync();

                return RedirectToAction("Edit", new { name = editMalzemeRequest.MalzemeName });

                //return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { name = editMalzemeRequest.MalzemeName });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditMalzemeRequest editMalzemeRequest)
        {
            var malzeme = await malzemeTakipDbContext.Malzemeler.FindAsync(editMalzemeRequest.Id);

            if (malzeme != null)
            {
                malzemeTakipDbContext.Malzemeler.Remove(malzeme);
                await malzemeTakipDbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { name = editMalzemeRequest.MalzemeName });

        }
    }
}
