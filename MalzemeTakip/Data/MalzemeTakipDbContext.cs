using MalzemeTakip.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MalzemeTakip.Data
{
    public class MalzemeTakipDbContext : DbContext
    {
        public MalzemeTakipDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Yemek> Yemekler { get; set; }
        public DbSet<Malzeme> Malzemeler { get; set; }

    }
}
