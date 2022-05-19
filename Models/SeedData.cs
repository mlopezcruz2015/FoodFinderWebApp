using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodFinderWebApp.Data;
using System;
using System.Linq;

namespace FoodFinderWebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FoodFinderWebAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FoodFinderWebAppContext>>()))
            {
                // Look for any movies.
                if (context.SavedFoodLocation.Any())
                {
                    return;   // DB has been seeded
                }

                context.SavedFoodLocation.AddRange(
                    new SavedFoodLocation
                    {
                        Name = "Burger King",
                        DateAdded = DateTime.Parse("1989-2-12"),
                        Address = "123 King Ave",
                    },

                    new SavedFoodLocation
                    {
                        Name = "McDonalds",
                        DateAdded = DateTime.Parse("1990-2-12"),
                        Address = "123 Buger Ave",
                    },

                    new SavedFoodLocation
                    {
                        Name = "Taco Bell",
                        DateAdded = DateTime.Parse("1991-2-12"),
                        Address = "123 Taco Ave",
                    },

                    new SavedFoodLocation
                    {
                        Name = "Sonic",
                        DateAdded = DateTime.Parse("1992-2-12"),
                        Address = "123 Sonic Ave",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}