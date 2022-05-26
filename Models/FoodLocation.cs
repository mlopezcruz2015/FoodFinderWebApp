using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodFinderWebApp.Models
{
    public class FoodLocation
    {

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }

        public string? Address { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string? Category { get; set; }
    }
}
