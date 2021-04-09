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
            //delete products when deleting user
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Owner)
                .WithMany(t => t.Products)
                .OnDelete(DeleteBehavior.Cascade);
            //unique constraint on username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(true);
            //username is case sensitive
            modelBuilder.Entity<User>().Property(c => c.UserName)
            .UseCollation("SQL_Latin1_General_CP1_CS_AS");
            //password is case sensitive
            modelBuilder.Entity<User>().Property(c => c.Password)
            .UseCollation("SQL_Latin1_General_CP1_CS_AS");
        }
    }
}
