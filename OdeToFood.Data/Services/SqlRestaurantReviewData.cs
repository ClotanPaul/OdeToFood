using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantReviewData : IRestaurantReviewData
    {

        private OdeToFoodDbContext db;


        public SqlRestaurantReviewData(OdeToFoodDbContext db)
        {
            this.db = db;
        }


        void IRestaurantReviewData.AddReview(RestaurantReview review)
        {
            var restaurant = db.Restaurants.FirstOrDefault(r => r.Id == review.RestaurantId);
            restaurant.Reviews.Add(review);
            db.SaveChanges();
        }

        void IRestaurantReviewData.ApproveReview(int reviewId)
        {
            var review = db.RestaurantReviews.FirstOrDefault(r => r.Id == reviewId);
            review.IsApproved = true;
            db.SaveChanges();
        }

        void IRestaurantReviewData.DeleteReview(int reviewId)
        {
            var review = db.RestaurantReviews.FirstOrDefault(r => r.Id == reviewId);
            db.RestaurantReviews.Remove(review);
            db.SaveChanges();
        }

        void IRestaurantReviewData.EditReview(RestaurantReview restaurantReview)
        {
            var currentReview = db.RestaurantReviews.FirstOrDefault(r => r.Id == restaurantReview.Id);

            if (currentReview != null)
            {
                currentReview.Grade = restaurantReview.Grade;
                currentReview.Review = restaurantReview.Review;
                currentReview.IsApproved = false;
            }
            db.SaveChanges();
        }

        RestaurantReview IRestaurantReviewData.Get(int reviewId)
        {
            var model = db.RestaurantReviews.FirstOrDefault(r=>r.Id == reviewId);
            return model;
        }

        List<RestaurantReview> IRestaurantReviewData.GetAll(int restaurantId)
        {
            return db.RestaurantReviews.Where(rr=>rr.RestaurantId == restaurantId).ToList();
        }

        List<RestaurantReview> IRestaurantReviewData.GetAllUnapprovedReviews()
        {
            var model = db.RestaurantReviews.Where(rr=>rr.IsApproved == false).ToList();
            return model;
        }
    }
}
