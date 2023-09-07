using MalzemeTakip.Models.Domain;

namespace MalzemeTakip.Repositories
{
    public interface IYemekRepository
    {
        Task<IEnumerable<Yemek?>> GetAllAsync();

        Task<Yemek?> GetAsync(string name);

        Task<Yemek> AddAsync(Yemek yemek);

        Task<Yemek?> UpdateAsync(Yemek yemek);

        Task<Yemek?> DeleteAsync(Guid id);
    }
}
