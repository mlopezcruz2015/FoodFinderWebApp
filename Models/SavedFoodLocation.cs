using System.ComponentModel.DataAnnotations;

namespace FoodFinderWebApp.Models
{
    public class SavedFoodLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
