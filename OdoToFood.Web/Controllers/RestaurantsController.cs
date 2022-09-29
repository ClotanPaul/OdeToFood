
using Microsoft.AspNet.Identity;
using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    [Authorize(Roles = "Admin, RestaurantOwner")]
    public class RestaurantsController : Controller
    {

        private readonly IRestaurantData db;

        public RestaurantsController()
        {

        }

        public RestaurantsController(IRestaurantData db)
        {
            this.db = db;
        }
        // GET: Restaurants
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        public ActionResult RestaurantOwnerIndex()
        {
            var userId = User.Identity.GetUserId();
            var model = db.GetRestaurant(userId);
            return View(model);
        }

        public ActionResult MyRestaurantDetails()
        {
            Restaurant model;
            var userId = User.Identity.GetUserId();
            model = db.GetRestaurant(userId);

            if(model == null)
            {
                return View("NotFound");
            }

            if (!model.isActive)
            {
                return View("ClosedRestaurant",model);
            }

            if (model == null)
                return View("NotFound");
            return View("Details", model);
        }

        public ActionResult Details(int id)
        {

            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var currentRestaurant = db.GetRestaurant(userId);
            if (currentRestaurant == null)
                return View();
            else
                return View("AlreadyOwnerOfRestaurant", currentRestaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.OwnerId = User.Identity.GetUserId();
                db.Add(restaurant);
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Get(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Update(restaurant);
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Get(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeactivateRestaurant(int id)
        {
            var restaurant = db.Get(id);
            return View(restaurant);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeactivateRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return View("NotFound");
            }

            db.DeactivateRestaurant(restaurant.Id, restaurant.DeactivationReason);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ActivateRestaurant(int id)
        {
            var restaurant = db.Get(id);
            return View(restaurant);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ActivateRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return View("NotFound");
            }

            db.ActivateRestaurant(restaurant.Id);
            return RedirectToAction("Index");
        }


    }
}