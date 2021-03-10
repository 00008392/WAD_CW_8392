using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WAD._8392.DAL.Models;

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
    }
}
