using Microsoft.AspNet.Identity;
using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OdoToFood.Web.Views
{
    [Authorize(Roles = "Admin, RestaurantOwner")]
    public class MenuController : Controller
    {
        private OdeToFoodDbContext db;
        private IRestaurantData restaurantDb;
        private IMenuData menuDb;
        public MenuController(OdeToFoodDbContext db, IRestaurantData restaurantDb, IMenuData menuDb)
        {
            this.db = db;
            this.restaurantDb = restaurantDb;
            this.menuDb = menuDb;
        }

        [AllowAnonymous]
        // GET: Menu
        public ActionResult ShowRestaurantMenu(int restaurantId)
        {
            var menus = menuDb.GetAll(restaurantId);

            if(menus.Count == 0)
            {
                return RedirectToAction("Create");
            }

            return View(menus);

        }

        [HttpGet]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var currentRestaurant = restaurantDb.GetRestaurant(userId);
            ViewData.Add(new KeyValuePair<string, object>("RestaurantId", currentRestaurant.Id));
            if (currentRestaurant == null)
                return View("NotFound", "Restaurants");
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantMenu menu)
        {
            var ownerId = User.Identity.GetUserId();
            var restaurant = restaurantDb.GetRestaurant(ownerId);
            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    menuDb.Add(menu, restaurant.Id);

                }
            }
            return RedirectToAction("ShowRestaurantMenu", new { restaurantId = restaurant.Id });
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = menuDb.GetRestaurantMenu(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            var menu = db.RestaurantMenus.FirstOrDefault(m => m.Id == id);
            var restaurantId = menu.RestaurantId;
            menuDb.Delete(id);
            return RedirectToAction("ShowRestaurantMenu", new { restaurantId });
        }

        public ActionResult Details(int id)
        {

            var model = menuDb.GetRestaurantMenu(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = menuDb.GetRestaurantMenu(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RestaurantMenu menu)
        {
            if (ModelState.IsValid)
            {
                menuDb.Update(menu);
                return RedirectToAction("Details", new { id = menu.Id });
            }
            return View();
        }
    }
}