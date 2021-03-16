using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;

namespace WAD._8392.DAL.Context
{
    public class MusicInstrumentsDbContext: DbContext
    {
        public MusicInstrumentsDbContext(DbContextOptions<MusicInstrumentsDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Owner)
                .WithMany(t => t.Products)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(true);
        }
    }
}
