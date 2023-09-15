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

        public YemekController(IMalzemeRepository malzemeRepository, IYemekRepository yemekRepository)
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
                MalzemeYemekler = malzemeler.OrderBy(x => x.MalzemeName).Select(x => new SelectListItem { Text = x.MalzemeName, Value = x.Id.ToString() })
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

            // MalzemeYemekleri temsil eden bir ICollection<MalzemeYemek> oluşturun
            //ICollection<MalzemeYemek> malzemeYemekler = new List<MalzemeYemek>();

            // Yemeğe ait MalzemeYemek koleksiyonunu oluşturun
            var malzemeYemekler = new List<MalzemeYemek>();

            foreach (var selectedMalzemeAdi in addYemekRequest.SelectedMalzemeler)
            {
                var existingMalzeme = await malzemeRepository.GetAsync(selectedMalzemeAdi);

                if (existingMalzeme != null)
                {
                    // Yemeğe bağlı MalzemeYemek nesnesini oluşturun
                    var malzemeYemek = new MalzemeYemek
                    {
                        Malzeme = existingMalzeme,
                        Yemek = yemek,
                        Miktar = (int)existingMalzeme.MalzemeMiktar // Malzeme miktarını ekleyin
                    };

                    // MalzemeYemekleri koleksiyonuna ekleyin
                    malzemeYemekler.Add(malzemeYemek);
                }
            }

            // Yemek nesnesine malzemeYemekler koleksiyonunu atayın
            yemek.MalzemeYemekler = malzemeYemekler;


            // Yemek nesnesinin MalzemeYemekler koleksiyonunu ayarlayın
            yemek.MalzemeYemekler = malzemeYemekler;

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
                    MalzemeYemekler = yemek.MalzemeYemekler
                };


                return View(editYemekRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditYemekRequest editYemekRequest)
        {
            // İlgili yemeği veritabanından alın
            var existingYemek = await yemekRepository.GetAsync(editYemekRequest.YemekName);

            if (existingYemek != null)
            {
                // Yemek adı ve diğer özelliklerini güncelle
                existingYemek.YemekName = editYemekRequest.YemekName;

                // Malzemeleri güncelle
                if (editYemekRequest.SelectedMalzemeYemekler != null && editYemekRequest.SelectedMalzemeYemekler.Any())
                {
                    // Seçilen malzemelerin listesini temizle
                    existingYemek.MalzemeYemekler.Clear();

                    foreach (var malzemeName in editYemekRequest.SelectedMalzemeYemekler)
                    {
                        // Malzeme adına göre malzemeyi veritabanından alın
                        var malzeme = await malzemeRepository.GetAsync(malzemeName);

                        if (malzeme != null)
                        {
                            // Malzeme-Yemek ilişkisini oluşturun ve miktarı ayarlayın
                            var malzemeYemek = new MalzemeYemek
                            {
                                Malzeme = malzeme,
                                Yemek = existingYemek,
                                Miktar = 1 // Burada miktarı uygun bir şekilde ayarlayabilirsiniz
                            };

                            // Malzemeyi yemeğe ekle
                            existingYemek.MalzemeYemekler.Add(malzemeYemek);
                        }
                    }
                }


                // Yemeği güncelle
                var updatedYemek = await yemekRepository.UpdateAsync(existingYemek);

                if (updatedYemek != null)
                {
                    // Başarı bildirimi göster
                }
                else
                {
                    // Hata bildirimi göster
                }

                return RedirectToAction("Edit", new { name = editYemekRequest.YemekName });
            }
            else
            {
                // Yemek bulunamadı hatası göster
                return RedirectToAction("Edit", new { name = editYemekRequest.YemekName });
            }
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
