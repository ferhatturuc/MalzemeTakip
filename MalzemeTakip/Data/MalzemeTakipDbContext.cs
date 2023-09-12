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
        public DbSet<MalzemeYemek> MalzemeYemekler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MalzemeYemek tablosunun özel anahtarını ayarlama
            modelBuilder.Entity<MalzemeYemek>()
                .HasKey(my => new { my.MalzemeId, my.YemekId });

            // MalzemeYemek ile Malzeme arasında ilişki
            modelBuilder.Entity<MalzemeYemek>()
                .HasOne(my => my.Malzeme)
                .WithMany(m => m.MalzemeYemekler)
                .HasForeignKey(my => my.MalzemeId);

            // MalzemeYemek ile Yemek arasında ilişki
            modelBuilder.Entity<MalzemeYemek>()
                .HasOne(my => my.Yemek)
                .WithMany(y => y.MalzemeYemekler)
                .HasForeignKey(my => my.YemekId);
        }

    }
}
