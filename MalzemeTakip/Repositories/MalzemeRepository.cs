using MalzemeTakip.Models.Domain;

namespace MalzemeTakip.Repositories
{
    public class MalzemeRepository : IMalzemeInterface
    {
        public Task<Malzeme> AddAsync(Malzeme malzeme)
        {
            throw new NotImplementedException();
        }

        public Task<Malzeme?> DeleteAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Malzeme>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Malzeme> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Malzeme?> UpdateAsync(Malzeme malzeme)
        {
            throw new NotImplementedException();
        }
    }
}
