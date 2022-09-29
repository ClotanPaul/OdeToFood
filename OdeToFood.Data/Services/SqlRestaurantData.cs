using OdeToFood.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext db;


        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        void IRestaurantData.ActivateRestaurant(int id)
        {
            var restaurant = db.Restaurants.FirstOrDefault(r => r.Id == id);
            restaurant.DeactivationReason = null;
            restaurant.isActive = true;
            db.SaveChanges();
        }

        void IRestaurantData.Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
        }

        void IRestaurantData.DeactivateRestaurant(int id, string deactivationReason)
        {
            var restaurant = db.Restaurants.Find(id);
            restaurant.DeactivationReason = deactivationReason;
            restaurant.isActive = false;
            db.SaveChanges();
        }

        void IRestaurantData.Delete(int id)
        {
            var restaurant = db.Restaurants.Find(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
            }
        }

        Restaurant IRestaurantData.Get(int id)
        {
            return db.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        IEnumerable<Restaurant> IRestaurantData.GetAll()
        {
            return db.Restaurants.AsEnumerable();
        }

        Restaurant IRestaurantData.GetRestaurant(string ownerId)
        {
            Restaurant restaurant = db.Restaurants.FirstOrDefault(r => r.OwnerId == ownerId);
            if (restaurant != null)
                return restaurant;
            else
                return null;
        }

        void IRestaurantData.Update(Restaurant restaurant)
        {
            //use optimistic concurrency ?!
            var databaseRestaurant = db.Restaurants.FirstOrDefault(r => r.Id == restaurant.Id);
            if (databaseRestaurant != null)
            {
                databaseRestaurant.Id = databaseRestaurant.Id;
                databaseRestaurant.Name = restaurant.Name ?? databaseRestaurant.Name;
                databaseRestaurant.Description = restaurant.Description ?? databaseRestaurant.Description;
                databaseRestaurant.Cuisine = restaurant.Cuisine;
                databaseRestaurant.Menus = restaurant.Menus ?? databaseRestaurant.Menus;
                restaurant.OwnerId = restaurant.OwnerId ?? databaseRestaurant.OwnerId;
            }
            db.SaveChanges();

        }
    }
}
