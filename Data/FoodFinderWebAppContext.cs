using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodFinderWebApp.Models;

namespace FoodFinderWebApp.Data
{
    public class FoodFinderWebAppContext : DbContext
    {
        public FoodFinderWebAppContext (DbContextOptions<FoodFinderWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<FoodFinderWebApp.Models.SavedFoodLocation>? SavedFoodLocation { get; set; }
    }
}
