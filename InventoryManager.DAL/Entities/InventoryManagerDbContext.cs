using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL.Entities
{
    public class InventoryManagerDbContext : DbContext
    {
        public InventoryManagerDbContext(DbContextOptions<InventoryManagerDbContext> options) : base(options)
        {

        }   

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email, "IX_UniqueEmail")
                .IsUnique();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .HasMaxLength(100)
               .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .HasMaxLength(15)
                .IsRequired();


            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            modelBuilder.Entity<Product>()
                .Property(p => p.BrandName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(s => s.Name) 
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(s => s.Quantity)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(s => s.Price)
                .IsRequired();
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
