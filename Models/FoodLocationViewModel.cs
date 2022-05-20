using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FoodFinderWebApp.Models
{
    public class FoodLocationViewModel
    {
        public List<SavedFoodLocation>? SavedFoodLocations { get; set; }
        public SelectList? Categories { get; set; }
        public string? FoodCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
