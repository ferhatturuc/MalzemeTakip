using MalzemeTakip.Models.Domain;

namespace MalzemeTakip.Repositories
{
    public interface IYemekRepository
    {
        Task<IEnumerable<Yemek?>> GetAllAsync();

        Task<Yemek?> GetAsync(string name);

        Task<Yemek> AddAsync(Yemek yemek);
        //Task<Yemek> AddAsync(string yemekName, string malzemeName, int malzemeMiktar);

        Task<Yemek?> UpdateAsync(Yemek yemek);

        Task<Yemek?> DeleteAsync(Guid id);
    }
}
