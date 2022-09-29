using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdeToFood.Data.Models
{
    public class RestaurantMenu
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Menu Name")]
        public string Name { get; set; }

        [Display(Name = "Menu Description")]
        public string Description { get; set; }

        [Display(Name = "Price(RON)")]
        public int Price { get; set; }


        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
