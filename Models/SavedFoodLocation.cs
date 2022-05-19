﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodFinderWebApp.Models
{
    public class SavedFoodLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
