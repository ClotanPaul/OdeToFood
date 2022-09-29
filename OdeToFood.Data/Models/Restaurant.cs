using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Data.Models
{
    public class Restaurant
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }

        [Display(Name = "Specific")]
        public CuisineType Cuisine { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string OwnerId { get; set; }

        public bool isActive { get; set ; }

        [Display(Name = "Suspension Reason")]
        public string DeactivationReason { get; set; }

        public virtual ICollection<RestaurantMenu> Menus { get; set; }

        public virtual ICollection<RestaurantReview> Reviews { get; set; }
    }
}
