using MalzemeTakip.Models.Domain;

namespace MalzemeTakip.Repositories
{
    public interface IMalzemeInterface
    {
        Task<IEnumerable<Malzeme>> GetAllAsync();
        Task<Malzeme> GetAsync(string name);
        Task<Malzeme> AddAsync(Malzeme malzeme);
        Task<Malzeme?> UpdateAsync(Malzeme malzeme);
        Task<Malzeme?> DeleteAsync(string name);

    }
}
