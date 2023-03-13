using InventoryManager.DAL.Entities;
using InventoryManager.DAL.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManager.DAL.Seeds
{
    public class SeedData
    {

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            InventoryManagerDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<InventoryManagerDbContext>();

            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(UsersWithProducts());
                await context.SaveChangesAsync();
            }


        }

        private static IEnumerable<User> UsersWithProducts()
        {
            return new List<User>()
            {
                new User()
                {
                    FullName = "Uzumaki Naruto",
                    Email = "uzumaki.naruto@domain.com",
                    Phone = "23490943989839",
                    Password = "123445678",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Name = "Nokia Express Music",
                            Description = "A portable, easy to use, flexible phone with an insane music player",
                            Category = Category.Phones,
                            Quantity = 200,
                            Price = 14500,
                            BrandName = "Nokia",
                            Sales = new List<Sales>()
                            {
                                new Sales
                                {
                                    Name = "Nokia Express Music",
                                     Category = Category.Phones,
                                     Quantity = 20,
                                     Price = 14500,

                                }
                            }

                        },

                        new Product
                        {
                             Name = "F9 Wireless Earbuds",
                            Description = "Fast charging bluetooth connecting wireless earbuds",
                            Category = Category.Accessories,
                            Quantity = 100,
                            Price = 4500,
                              BrandName = "Toshiba"

                        },
                        new Product
                        {
                            Name = "Dell XPS",
                            Description = "A very fine small dell",
                            Category = Category.Computing,
                            Quantity = 50,
                            Price = 145000,
                              BrandName = "Dell"

                        }
                    }

                },
                new User()
                {

                    FullName = "Sasuke Uchiha",
                    Email = "sasuke.uchiha@domain.com",
                    Phone = "23490943989838",
                    Password = "6789012345",
                    Products = new List<Product>()
                    {
                        new Product
                        {

                            Name = "White Tshirt",
                            Description = "An all white coperate collar shirt",
                            Category = Category.Beauty,
                            Quantity = 70,
                            Price = 10000,
                            BrandName = "LV"

                        },

                        new Product
                        {
                            Name = "Black Slide",
                            Description = "Blade top cover slides",
                            Category = Category.Beauty,
                            Quantity = 50,
                            Price = 6000,
                            BrandName = "Adidas"

                        },
                        new Product
                        {
                            Name = "Ps5 God of War",
                            Description = "god of war gaming cd",
                            Category = Category.Gaming,
                            Quantity = 30,
                            Price = 16000,
                            BrandName = "Konami"

                        }
                    }

                }
            };
        }
    }
}
