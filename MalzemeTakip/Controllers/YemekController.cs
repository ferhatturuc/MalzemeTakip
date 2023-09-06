/*using MalzemeTakip.Data;
using MalzemeTakip.Models.Domain;
using MalzemeTakip.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MalzemeTakip.Controllers
{
    public class YemekController : Controller
    {
        private readonly MalzemeTakipDbContext malzemeTakipDbContext;

        public YemekController(MalzemeTakipDbContext malzemeTakipDbContext)
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
        public async Task<IActionResult> Add(AddYemekRequest addYemekRequest)
        {
            // Mapping AddMalzemeRequest to Malzeme domain model
            var yemek = new Yemek
            {
                YemekName = addYemekRequest.YemekName
            };

            await malzemeTakipDbContext.Yemekler.AddAsync(yemek);
            await malzemeTakipDbContext.SaveChangesAsync();

            return RedirectToAction("List");
            //return View("Add");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var yemek = await malzemeTakipDbContext.Yemekler.ToListAsync();

            return View(yemek);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            //var malzeme =malzemeTakipDbContext.Malzemeler.Find(malzemeName);

            var yemek = await malzemeTakipDbContext.Yemekler.FirstOrDefaultAsync(m => m.YemekName == name);

            if (yemek != null)
            {
                var editYemekRequest = new EditYemekRequest
                {
                    Id = yemek.Id,
                    //YemekName = yemek.YemekName
                };

                return View(editYemekRequest);
            }

            return View(null);
        }
    }
}
*/