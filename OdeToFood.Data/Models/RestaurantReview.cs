using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Models
{
    public class RestaurantReview
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Stars")]
        [Range(1, 5, ErrorMessage = "Value for Grade must be between 1 and 5.")]
        public int Grade { get; set; }

        [Display(Name = "Comments")]
        public string Review { get; set; }
        
        public int RestaurantId { get; set; }
        public string UserId { get; set; }

        public bool IsApproved { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
