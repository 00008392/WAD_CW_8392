using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAD._8392.DAL.Context;
using WAD._8392.DAL.DBO;

namespace WAD._8392.DAL.Seeding
{
    public class Seed
    {
        //class for populating database with sample data
        public static void Initialize(MusicInstrumentsDbContext context)
        {
            if (!context.Users.Any())
            {
                //populating with users
                var users = new List<User>()
         {
            new User {  FirstName = "Sara", LastName = "O'Neil", DateOfBirth = DateTime.Parse("12-01-2000", CultureInfo.InvariantCulture),
                        PhoneNumber = "1234567", UserName = "SaraN", Email = "sara@gmail.com", Password = "12345678"},
            new User {   FirstName = "John", LastName = "Hall", DateOfBirth = DateTime.Parse("07-20-2000", CultureInfo.InvariantCulture),
                        PhoneNumber = "7654321", UserName = "WilliamJohn", Email = "william@gmail.com", Password = "87654321" },
            new User {   FirstName = "Mike", LastName="Smith", DateOfBirth = DateTime.Parse("02-01-1978", CultureInfo.InvariantCulture),
                         PhoneNumber = "5674321", UserName="Michael", Email="msmth@gmail.com", Password="ABCD5679021"},
            new User {   FirstName = "Laya", LastName="Burnell", DateOfBirth = DateTime.Parse("02-01-1998", CultureInfo.InvariantCulture),
                         PhoneNumber = "3452187", UserName="Lale", Email="layaburn@gmail.com", Password="KLS98765#"},
            new User {   FirstName = "Toby", LastName="Kavanaugh", DateOfBirth = DateTime.Parse("06-30-1995", CultureInfo.InvariantCulture),
                         PhoneNumber = "7632456", UserName="TobyK", Email="kavanaugh@gmail.com", Password="TB22334455#"}
         };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Manufacturers.Any())
            {
                //populating with manufacturers
                var manufacturers = new List<Manufacturer>()
         {
            new Manufacturer { ManufacturerName = "Gibson"},
            new Manufacturer { ManufacturerName = "Yamaha"},
            new Manufacturer { ManufacturerName = "Roland"},
            new Manufacturer { ManufacturerName = "Kawai"},
            new Manufacturer { ManufacturerName = "Fender"},
            new Manufacturer { ManufacturerName = "Martin"},
            new Manufacturer { ManufacturerName = "Riga"},
            new Manufacturer { ManufacturerName = "Belarus"},
            new Manufacturer { ManufacturerName = "Alvarez"},
            new Manufacturer { ManufacturerName = "Cort"},
            new Manufacturer { ManufacturerName = "Baldwin"},
            new Manufacturer { ManufacturerName = "Taylor"},
            new Manufacturer { ManufacturerName = "Stentor"},
            new Manufacturer { ManufacturerName = "Cremona"},
            new Manufacturer { ManufacturerName = "Cecilo"},
            new Manufacturer { ManufacturerName = "Mendini"},
            new Manufacturer { ManufacturerName = "Casio"},
            new Manufacturer { ManufacturerName = "Other"}

         };
                context.Manufacturers.AddRange(manufacturers);
                context.SaveChanges();
            }
            if (!context.ProductCategories.Any())
            {
                //populating with categories
                var productCategories = new List<ProductCategory>()
          {
             new ProductCategory {CategoryName = "String"},
             new ProductCategory {CategoryName = "Wind"},
             new ProductCategory {CategoryName = "Keyboard"},
             new ProductCategory {CategoryName = "Percussion"},
             new ProductCategory {CategoryName = "Brass"},
             new ProductCategory {CategoryName = "Electronic"},
             new ProductCategory {CategoryName = "Other"}

          };
                context.ProductCategories.AddRange(productCategories);
                context.SaveChanges();
            }
            if (!context.ProductSubcategories.Any())
            {
                //extracting categories to set correct foreign keys in subcategories
                var str = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "String").ProductCategoryId;
                var wind = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "Wind").ProductCategoryId;
                var key = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "Keyboard").ProductCategoryId;
                var percussion = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "Percussion").ProductCategoryId;
                var brass = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "Brass").ProductCategoryId;
                var electro = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "Electronic").ProductCategoryId;
                var other = context.ProductCategories.FirstOrDefault(x => x.CategoryName == "Other").ProductCategoryId;
                //populating with subcategories
                var productSubcategories = new List<ProductSubcategory>()
          {
             new ProductSubcategory { SubcategoryName ="Violin", ProductCategoryId = str},
             new ProductSubcategory { SubcategoryName ="Guitar", ProductCategoryId = str},
             new ProductSubcategory { SubcategoryName ="Cello", ProductCategoryId = str},
             new ProductSubcategory { SubcategoryName ="Double Bass", ProductCategoryId = str},
             new ProductSubcategory { SubcategoryName ="Harp", ProductCategoryId = str},
             new ProductSubcategory { SubcategoryName ="Flute", ProductCategoryId = wind},
             new ProductSubcategory { SubcategoryName ="Clarinet", ProductCategoryId = wind},
             new ProductSubcategory { SubcategoryName ="Contrabassoon", ProductCategoryId =wind},
             new ProductSubcategory { SubcategoryName ="Piano", ProductCategoryId = key},
             new ProductSubcategory { SubcategoryName ="Organ", ProductCategoryId = key},
             new ProductSubcategory { SubcategoryName ="Accordion", ProductCategoryId = key},
             new ProductSubcategory { SubcategoryName ="Drum", ProductCategoryId = percussion},
             new ProductSubcategory { SubcategoryName ="Trumpet", ProductCategoryId = brass},
             new ProductSubcategory { SubcategoryName ="Trombone", ProductCategoryId = brass},
             new ProductSubcategory { SubcategoryName ="Tuba", ProductCategoryId = brass},
             new ProductSubcategory { SubcategoryName ="Electronic guitar", ProductCategoryId = electro},
             new ProductSubcategory { SubcategoryName ="Digital piano", ProductCategoryId = electro},
             new ProductSubcategory { SubcategoryName ="Syntezator", ProductCategoryId = electro},
             new ProductSubcategory { SubcategoryName ="Digital Grand piano", ProductCategoryId = electro},
             new ProductSubcategory { SubcategoryName ="Grand piano", ProductCategoryId = key},
             new ProductSubcategory { SubcategoryName ="Other", ProductCategoryId = other},
          };
                context.ProductSubcategories.AddRange(productSubcategories);
                context.SaveChanges();
            }
            if (!context.Products.Any())
            {
                //extracting manufacturers to set correct foreign keys in products
                var roland = context.Manufacturers.FirstOrDefault(x => x.ManufacturerName == "Roland").ManufacturerId;
                var casio = context.Manufacturers.FirstOrDefault(x => x.ManufacturerName == "Casio").ManufacturerId;
                var taylor = context.Manufacturers.FirstOrDefault(x => x.ManufacturerName == "Taylor").ManufacturerId;
                var yamaha = context.Manufacturers.FirstOrDefault(x => x.ManufacturerName == "Yamaha").ManufacturerId;
                //extracting subcategories to set correct foreign keys in products
                var digital = context.ProductSubcategories.FirstOrDefault(x => x.SubcategoryName == "Digital piano").ProductSubcategoryId;
                var syntezator = context.ProductSubcategories.FirstOrDefault(x => x.SubcategoryName == "Syntezator").ProductSubcategoryId;
                var violin = context.ProductSubcategories.FirstOrDefault(x => x.SubcategoryName == "Violin").ProductSubcategoryId;
                var guitar = context.ProductSubcategories.FirstOrDefault(x => x.SubcategoryName == "Guitar").ProductSubcategoryId;
                var grand = context.ProductSubcategories.FirstOrDefault(x => x.SubcategoryName == "Digital Grand piano").ProductSubcategoryId;
                //populating with products
                var products = new List<Product>()
          {
             new Product { ProductName = "ROLAND HP603 CB", ProductDescription=@" built-in wireless Bluetooth® MIDI support for working with apps like Roland’s 
                                                                                Piano Partner 2 and Piano Designer on your smartphone or tablet. The HP603A also includes Bluetooth audio support, 
                                                                                allowing you to wirelessly stream music from your mobile device through the piano’s integrated sound system.",
                           Condition = Condition.New, DatePublished = DateTime.Parse("12-12-2020", CultureInfo.InvariantCulture), Location="Some Street, 4, 11",
                           ManufacturerId = roland, UserId = 1, Price = 2762, ProductSubcategoryId=digital, Status = Status.Available},
             new Product { ProductName = "Casio CT-X3000", ProductDescription=@" Almost new syntezator of high quality for affordable price",
                           Condition = Condition.New, DatePublished = DateTime.Parse("3-13-2021", CultureInfo.InvariantCulture), Location="Some other Street, 5, 61",
                           ManufacturerId = casio, UserId = 2, Price = 400, ProductSubcategoryId=syntezator, Status = Status.Available},
             new Product { ProductName = "Taylor Baby BT2 Left Handed Acoustic Travel Guitar", ProductDescription=@" Designed to be a high-quality sonic companion for travelling, perfect for life on the road",
                           Condition = Condition.Medium, DatePublished = DateTime.Parse("3-09-2021", CultureInfo.InvariantCulture), Location="Somewhere, 5b, 81",
                           ManufacturerId = taylor, UserId = 3, Price = 359, ProductSubcategoryId=guitar, Status = Status.Available},
             new Product { ProductName = "Yamaha V10SG Intermediate Violin Pack, Full Size", ProductDescription=@" Complete with a case, Pernambuco bow, and quality Dominant strings",
                           Condition = Condition.Old, DatePublished = DateTime.Parse("3-07-2021", CultureInfo.InvariantCulture), Location="Somewhere, 5a, 32",
                           ManufacturerId = yamaha, UserId = 4, Price = 1099, ProductSubcategoryId=violin, Status = Status.Available},
             new Product { ProductName = "Roland GP607 Digital Grand Piano, Polished White", ProductDescription=@" Stream Music Through the Piano's Speakers with Bluetooth Audio",
                           Condition = Condition.Medium, DatePublished = DateTime.Parse("3-01-2021", CultureInfo.InvariantCulture), Location="Somewhere, 7, 45",
                           ManufacturerId = roland, UserId = 5, Price = 4732, ProductSubcategoryId=grand, Status = Status.Available}
          };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
