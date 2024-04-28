using GuitarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryName = "Guitars" },
                    new Category { CategoryName = "Basses" },
                    new Category { CategoryName = "Drums" }
                    );
            }

            context.SaveChanges();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        CategoryID = 1,
                        Code = "strat",
                        Name = "Fender Stratocaster",
                        Price = (decimal)699.00
                    },
                    new Product
                    {
                        CategoryID = 1,
                        Code = "les_paul",
                        Name = "Gibson Les Paul",
                        Price = (decimal)1199.00
                    },
                    new Product
                    {
                        CategoryID = 1,
                        Code = "sg",
                        Name = "Gibson SG",
                        Price = (decimal)2517.00
                    },
                    new Product
                    {
                        CategoryID = 1,
                        Code = "fg700s",
                        Name = "Yamaha FG700S",
                        Price = (decimal)489.99
                    },
                    new Product
                    {
                        CategoryID = 1,
                        Code = "washburn",
                        Name = "Washburn D10S",
                        Price = (decimal)299.00
                    },
                    new Product
                    {
                        CategoryID = 1,
                        Code = "rodriguez",
                        Name = "Rodriguez Caballero 11",
                        Price = (decimal)415.00
                    },
                    new Product
                    {
                        CategoryID = 2,
                        Code = "precision",
                        Name = "Fender Precision",
                        Price = (decimal)799.99
                    },
                    new Product
                    {
                        CategoryID = 2,
                        Code = "hofner",
                        Name = "Hofner Icon",
                        Price = (decimal)499.99
                    },
                    new Product
                    {
                        CategoryID = 3,
                        Code = "ludwig",
                        Name = "Ludwig 5-piece Drum Set with Cymbals",
                        Price = (decimal)699.99
                    },
                    new Product
                    {
                        CategoryID = 3,
                        Code = "tama",
                        Name = "Tama 5-Piece Drum Set with Cymbals",
                        Price = (decimal)799.99
                    }
                    );
            }

            context.SaveChanges();
        }
    }
}
