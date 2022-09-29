using OdeToFood.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class SqlMenuData : IMenuData
    {
        private OdeToFoodDbContext db;
        private IRestaurantData restaurantDb;
        public SqlMenuData(OdeToFoodDbContext db, IRestaurantData restaurantDb)
        {
            this.db = db;
            this.restaurantDb = restaurantDb;
        }
        void IMenuData.Add(RestaurantMenu menu, int restaurantId)
        {
            var currentRestaurant = restaurantDb.Get(restaurantId);
            if (currentRestaurant != null)
            {

                if (currentRestaurant.Menus == null)
                {
                    currentRestaurant.Menus = new List<RestaurantMenu>();
                }
                currentRestaurant.Menus.Add(menu);
                db.SaveChanges();
            }
        }

        void IMenuData.Delete(int menuId)
        {
            var menu = db.RestaurantMenus.FirstOrDefault(m => m.Id == menuId);
            db.RestaurantMenus.Remove(menu);
            db.SaveChanges();
        }


        List<RestaurantMenu> IMenuData.GetAll(int restaurantId)
        {
            var restaurant = restaurantDb.Get(restaurantId);
            var menus = restaurant.Menus.ToList(); 
            return menus;

        }

        RestaurantMenu IMenuData.GetRestaurantMenu(int menuId)
        {
            var menu = db.RestaurantMenus.FirstOrDefault(rm => rm.Id == menuId);
            return menu;
        }

        void IMenuData.Update(RestaurantMenu menu)
        {
            var currentMenu = db.RestaurantMenus.FirstOrDefault(m => m.Id == menu.Id);
            if (currentMenu != null)
            {
                currentMenu.Name = menu.Name;
                currentMenu.Description = menu.Description;
                currentMenu.Price = menu.Price;
                db.SaveChanges();
            }
        }
    }
}
