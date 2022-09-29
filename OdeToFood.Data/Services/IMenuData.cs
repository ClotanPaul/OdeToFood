using OdeToFood.Data.Models;
using System.Collections.Generic;

namespace OdeToFood.Data.Services
{
    public interface IMenuData
    {
        List<RestaurantMenu> GetAll(int restaurantId);

        void Add(RestaurantMenu menu, int restaurantId);

        void Update(RestaurantMenu menu);

        void Delete(int menuId);

        RestaurantMenu GetRestaurantMenu(int menuId);
    }
}
