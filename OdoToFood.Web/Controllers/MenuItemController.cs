using Microsoft.AspNet.Identity;
using OdeToFood.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OdoToFood.Web.Controllers
{
    [Authorize]
    public class MenuItemController : Controller
    {
        private OdeToFoodDbContext db;
        private IMenuData menuDb;
        private IMenuItemData menuItemDb;
        private IRestaurantData restaurantDb;
        public MenuItemController(OdeToFoodDbContext db, IRestaurantData restaurantDb, IMenuData menuDb, IMenuItemData menuItemDb)
        {
            this.db = db;
            this.restaurantDb = restaurantDb;
            this.menuDb = menuDb;
            this.menuItemDb = menuItemDb;
        }

        // GET: MenuIItem

        public ActionResult Index(int menuId)
        {
            var menuItems = menuItemDb.GetAll(menuId);

            if (menuItems.Count == 0)
            {
                return RedirectToAction("Create", new {menuId = menuId});
            }

            var userId = User.Identity.GetUserId();
            var restaurantId = menuItems.First().Menu.RestaurantId;

            ViewData.Add(new KeyValuePair<string, object>("menuId", menuId));
            ViewData.Add(new KeyValuePair<string, object>("restaurantId", restaurantId));

            return View(menuItems);
        }

        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpGet]
        public ActionResult Create(int menuId)
        {
            var currentMenu = menuDb.GetRestaurantMenu(menuId);
            ViewData.Add(new KeyValuePair<string, object>("menuId", menuId));
            if (currentMenu == null)
                return View("NotFound", "Menu");
            else
                return View();
        }

        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OdeToFood.Data.Models.MenuItem menuItem, int menuId)
        {
            if (ModelState.IsValid)
            {
                menuItemDb.Add(menuItem, menuId);
            }
            return RedirectToAction("Index", new { menuId = menuId });
        }


        [Authorize(Roles = "Admin,RestaurantOwner")]
        public ActionResult Details(int id)
        {

            var model = menuItemDb.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }


        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpGet]
        public ActionResult Delete(int menuItemId)
        {
            var model = menuItemDb.Get(menuItemId);
            if (model == null)
                return View("NotFound");
            return View(model);
        }


        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int menuItemId, FormCollection form)
        {
            var menuItem = db.MenuItems.FirstOrDefault(m => m.Id == menuItemId);
            menuItemDb.Delete(menuItemId);
            return RedirectToAction("Index", new { menuId = menuItem.RestaurantMenuId });
        }

        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpGet]
        public ActionResult Edit(int menuItemId)
        {
            var model = menuItemDb.Get(menuItemId);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpPost]
        public ActionResult Edit(OdeToFood.Data.Models.MenuItem menuItem)
        {
            menuItemDb.Edit(menuItem);
            var menuId = db.MenuItems.First(m => m.Id == menuItem.Id).RestaurantMenuId;
            return RedirectToAction("Index", new { menuId = menuId });
        }
    }
}