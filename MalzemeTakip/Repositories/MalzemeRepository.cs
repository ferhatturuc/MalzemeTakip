using MalzemeTakip.Data;
using MalzemeTakip.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MalzemeTakip.Repositories
{
    public class MalzemeRepository : IMalzemeRepository
    {
        private readonly MalzemeTakipDbContext malzemeTakipDbContext;

        public MalzemeRepository(MalzemeTakipDbContext malzemeTakipDbContext)
        {
            this.malzemeTakipDbContext = malzemeTakipDbContext;
        }

        public async Task<Malzeme> AddAsync(Malzeme Malzeme)
        {
            await malzemeTakipDbContext.Malzemeler.AddAsync(Malzeme);
            await malzemeTakipDbContext.SaveChangesAsync();
            return Malzeme;
        }

        public async Task<Malzeme?> DeleteAsync(int? id)
        {
            var existingMalzeme = await malzemeTakipDbContext.Malzemeler.FindAsync(id);

            if (existingMalzeme != null)
            {
                malzemeTakipDbContext.Malzemeler.Remove(existingMalzeme);
                await malzemeTakipDbContext.SaveChangesAsync();
                return existingMalzeme;
            }

            return null;
        }

        public async Task<IEnumerable<Malzeme>> GetAllAsync()
        {
            return await malzemeTakipDbContext.Malzemeler.ToListAsync();
        }

        public Task<Malzeme?> GetAsync(string name)
        {
            return malzemeTakipDbContext.Malzemeler.FirstOrDefaultAsync(x => x.MalzemeName == name);
        }

        public async Task<Malzeme?> UpdateAsync(Malzeme Malzeme)
        {
            var existingMalzeme = await malzemeTakipDbContext.Malzemeler.FindAsync(Malzeme.Id);

            if (existingMalzeme != null)
            {
                existingMalzeme.MalzemeName = Malzeme.MalzemeName;

                await malzemeTakipDbContext.SaveChangesAsync();

                return existingMalzeme;
            }

            return null;
        }
    }
}
