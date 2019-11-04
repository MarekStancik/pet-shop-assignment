using Microsoft.EntityFrameworkCore;
using PetShopApp.Core.Entities;

namespace PetShopApp.Infrastructure.SQLData
{

    public class PetShopAppContext : DbContext
    {
        public PetShopAppContext(DbContextOptions<PetShopAppContext> opt) : base(opt)
        {

        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PetColor> PetColors { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            // Pet table config
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PreviousOwner)
                .WithMany(o => o.pets)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.pets)
                .WithOne(p => p.PreviousOwner)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PetColor>()
                .HasKey(pc => new { pc.PetId,pc.ColorId});

            modelBuilder.Entity<PetColor>()
                .HasOne(pc => pc.Pet)
                .WithMany(p => p.PetColors)
                .HasForeignKey(pc => pc.PetId);

            modelBuilder.Entity<PetColor>()
                .HasOne(pc => pc.Color)
                .WithMany(c => c.PetColors)
                .HasForeignKey(pc => pc.ColorId);

            modelBuilder.Entity<User>()
                .HasKey(p => p.Username);
        }
    }
}