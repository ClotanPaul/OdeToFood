using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Data.Models
{
    public class MenuItem
    {
        [Required]
        public int Id { get; set; }

        [Display(Name="Dish name")]
        public string Name { get; set; }

        [Display(Name = "Nutritional Value")]
        public string NutritionalValue { get; set; }

        [Display(Name = "Energetic Value(kCal)")]
        public int Calories { get; set; }

        [Display(Name = "Vegan?")]
        public bool IsVegan { get; set; }

        [Display(Name = "Weight(g)")]
        public int Quantity { get; set; }

        [Display(Name = "Price(RON)")]
        public int Price { get; set; }

        public int RestaurantMenuId { get; set; }

        public virtual RestaurantMenu Menu { get; set; }
    }
}
