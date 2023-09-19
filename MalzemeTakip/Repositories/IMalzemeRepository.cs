using MalzemeTakip.Models.Domain;

namespace MalzemeTakip.Repositories
{
    public interface IMalzemeRepository
    {
        Task<IEnumerable<Malzeme?>> GetAllAsync();
        Task<Malzeme> GetAsync(int id);
        Task<Malzeme> AddAsync(Malzeme malzeme);
        Task<Malzeme?> UpdateAsync(Malzeme malzeme);
        Task<Malzeme?> DeleteAsync(int? id);
        public Malzeme GetMalzemeById(int malzemeId);
    }
}
