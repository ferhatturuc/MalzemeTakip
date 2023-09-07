using MalzemeTakip.Data;
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
                existingYemek.Malzemeler = yemek.Malzemeler;

                await malzemeTakipDbContext.SaveChangesAsync();

                return existingYemek;
            }

            return null;
        }
    }
}
