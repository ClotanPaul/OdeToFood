using OdeToFood.Data.Models;
using System.Collections.Generic;

namespace OdeToFood.Data.Services
{
    public interface IMenuItemData
    {
        List<MenuItem> GetAll(int menuId);

        void Add(MenuItem item, int menuId);

        MenuItem Get(int menuId);

        void Delete(int menuId);

        void Edit(MenuItem menuItem);
    }
}
