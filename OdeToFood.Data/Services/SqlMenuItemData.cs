using OdeToFood.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class SqlMenuItemData : IMenuItemData
    {

        private OdeToFoodDbContext db;
        private IMenuData menuDb;
        private IRestaurantData restaurantDb;
        public SqlMenuItemData(OdeToFoodDbContext db, IRestaurantData restaurantDb, IMenuData menuDb)
        {
            this.db = db;
            this.restaurantDb = restaurantDb;
            this.menuDb = menuDb;
        }

        List<MenuItem> IMenuItemData.GetAll(int menuId)
        {
            var menu = db.RestaurantMenus.FirstOrDefault(m => m.Id == menuId);
            var menuItems = menu.MenuItems.ToList();
            return menuItems;
        }



        void IMenuItemData.Add(MenuItem menuItem, int menuId)
        {
            var menu = menuDb.GetRestaurantMenu(menuId);
            menu.MenuItems.Add(menuItem);
            db.SaveChanges();
        }

        MenuItem IMenuItemData.Get(int menuItemId)
        {
            return db.MenuItems.FirstOrDefault(m => m.Id == menuItemId);
        }

        void IMenuItemData.Delete(int menuitemid)
        {
            var item = db.MenuItems.FirstOrDefault(m => m.Id == menuitemid);
            db.MenuItems.Remove(item);
            db.SaveChanges();
        }

        void IMenuItemData.Edit(MenuItem menuItem)
        {
            var currentMenuItem = db.MenuItems.FirstOrDefault(mi => mi.Id == menuItem.Id);
            if (currentMenuItem != null)
            {
                currentMenuItem.Name = menuItem.Name;
                currentMenuItem.NutritionalValue = menuItem.NutritionalValue;
                currentMenuItem.Calories = menuItem.Calories;
                currentMenuItem.IsVegan = menuItem.IsVegan;
                currentMenuItem.Quantity = menuItem.Quantity;
                currentMenuItem.Price = menuItem.Price;
            }
            db.SaveChanges();
        }
    }
}
