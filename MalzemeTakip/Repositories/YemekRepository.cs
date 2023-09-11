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

        public YemekRepository(MalzemeTakipDbContext context)
        {
            malzemeTakipDbContext = context;
        }

        public async Task<IEnumerable<Yemek>> GetAllAsync()
        {
            return await malzemeTakipDbContext.Yemekler.Include(y => y.Malzemeler).ToListAsync();
        }

        public async Task<Yemek?> GetAsync(string name)
        {
            return await malzemeTakipDbContext.Yemekler.Include(y => y.Malzemeler).FirstOrDefaultAsync(y => y.YemekName == name);
        }

        public async Task<Yemek> AddAsync(Yemek yemek)
        {
            await malzemeTakipDbContext.AddAsync(yemek);
            await malzemeTakipDbContext.SaveChangesAsync();
            return yemek;
        }

      /*  public async Task<Yemek> AddAsync(string yemekName, string malzemeName, int malzemeMiktar)
        {
            var yemek = await GetAsync(yemekName);
            var malzeme = await malzemeTakipDbContext.Malzemeler.FirstOrDefaultAsync(m => m.MalzemeName == malzemeName);

            if (yemek == null || malzeme == null)
            {
                return null;
            }

            malzeme.MalzemeMiktar = malzemeMiktar;
            yemek.Malzemeler.Add(malzeme);

            await malzemeTakipDbContext.SaveChangesAsync();

            return yemek;
        }*/

        public async Task<Yemek?> UpdateAsync(Yemek yemek)
        {
            var existingYemek = await malzemeTakipDbContext.Yemekler.Include(x => x.Malzemeler).FirstOrDefaultAsync(x => x.Id == yemek.Id);
            if (existingYemek != null)
            {
                existingYemek.Id= yemek.Id;
                existingYemek.YemekName = yemek.YemekName;

                existingYemek.Malzemeler = yemek.Malzemeler;

                await malzemeTakipDbContext.SaveChangesAsync();
                return existingYemek;
            }

            return null;
        }

        public async Task<Yemek?> DeleteAsync(Guid id)
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
    }
}


/*using MalzemeTakip.Data;
using MalzemeTakip.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MalzemeTakip.Repositories
{
    public class YemekRepository : IYemekRepository
    {
        private readonly MalzemeTakipDbContext malzemeTakipDbContext;

        public YemekRepository(MalzemeTakipDbContext malzemeTakipDbContext)
        {
            this.malzemeTakipDbContext = malzemeTakipDbContext;
        }
        public async Task<Yemek> AddAsync(Yemek yemek)
        {
            await malzemeTakipDbContext.AddAsync(yemek);
            await malzemeTakipDbContext.SaveChangesAsync();
            return yemek;
        }

        public async Task<Yemek?> DeleteAsync(Guid id)
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
        public async Task<IEnumerable<Yemek>> GetAllAsync()
        {
            return await malzemeTakipDbContext.Yemekler.Include(x => x.Malzemeler).ToListAsync();
        }

        public Task<Yemek?> GetAsync(string name)
        {
            return malzemeTakipDbContext.Yemekler.FirstOrDefaultAsync(x => x.YemekName == name);
        }

        public async Task<Yemek?> UpdateAsync(Yemek yemek)
        {
            var existingYemek = await malzemeTakipDbContext.Yemekler.FindAsync(yemek.Id);

            if (existingYemek != null)
            {
                existingYemek.YemekName = yemek.YemekName;
               // existingYemek.Malzemeler = yemek.Malzemeler;

                await malzemeTakipDbContext.SaveChangesAsync();

                return existingYemek;
            }

            return null;
        }
    }
}
*/