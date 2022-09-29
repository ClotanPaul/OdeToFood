using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Models
{
    public interface IRestaurantReviewData
    {
        void AddReview(RestaurantReview review);

        void EditReview(RestaurantReview review);

        void DeleteReview(int reviewId);

        void ApproveReview(int reviewId);

        RestaurantReview Get(int reviewId);

        List<RestaurantReview> GetAll(int restaurantId);

        List<RestaurantReview> GetAllUnapprovedReviews();
    }
}
