using MalzemeTakip.Data;
using MalzemeTakip.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MalzemeTakip.Repositories
{
    public class YemekRepository : IYemekRepository
    {
        private readonly MalzemeTakipDbContext malzemeTakipDbContext;

        public YemekRepository(MalzemeTakipDbContext malzemeTakipDbContext)
        {
            this.malzemeTakipDbContext = malzemeTakipDbContext;
        }

        public async Task<IEnumerable<Yemek>> GetAllAsync()
        {
            return await malzemeTakipDbContext.Yemekler.Include(y => y.MalzemeYemekler).ToListAsync();
        }

        public async Task<Yemek?> GetAsync(string name)
        {
            return await malzemeTakipDbContext.Yemekler.Include(y => y.MalzemeYemekler).FirstOrDefaultAsync(y => y.YemekName == name);
        }

        public async Task<Yemek> AddAsync(Yemek yemek)
        {
            await malzemeTakipDbContext.AddAsync(yemek);
            await malzemeTakipDbContext.SaveChangesAsync();
            return yemek;
        }

      

        public async Task<Yemek?> UpdateAsync(Yemek yemek)
        {
            var existingYemek = await malzemeTakipDbContext.Yemekler.Include(x => x.MalzemeYemekler).FirstOrDefaultAsync(x => x.Id == yemek.Id);
            
            if (existingYemek != null)
            {
                existingYemek.Id= yemek.Id;
                existingYemek.YemekName = yemek.YemekName;

                existingYemek.MalzemeYemekler = yemek.MalzemeYemekler;

                await malzemeTakipDbContext.SaveChangesAsync();
                return existingYemek;
            }

            return null;
        }

        public async Task<Yemek?> DeleteAsync(int id)
        {
            var existingYemek = await malzemeTakipDbContext.Yemekler.FindAsync(id);
    

            if (existingYemek != null)
            {
                malzemeTakipDbContext.Yemekler.Remove(existingYemek);
                await malzemeTakipDbContext.SaveChangesAsync();

                return existingYemek;
            }

            return null;
        }

        public async Task<Yemek> AddAsync(string yemekName, List<Malzeme> malzemeler)
        {
            // Yemek nesnesini tanımlayın ve yemek adını ayarlayın
            var yemek = new Yemek
            {
                YemekName = yemekName
            };

            ICollection<MalzemeYemek> malzemeYemekler = new List<MalzemeYemek>();

            foreach (var malzeme in malzemeler)
            {
                // Yemeğe bağlı MalzemeYemek nesnesini oluşturun
                var malzemeYemek = new MalzemeYemek
                {
                    Malzeme = malzeme,
                    Yemek = yemek,
                    Miktar = (int)malzeme.MalzemeMiktar // Malzeme miktarını ekleyin
                };

                // MalzemeYemekleri koleksiyonuna ekleyin
                yemek.MalzemeYemekler = malzemeYemekler;
            }

            await malzemeTakipDbContext.AddAsync(yemek);
            await malzemeTakipDbContext.SaveChangesAsync();

            return yemek;
        }
    }
}
