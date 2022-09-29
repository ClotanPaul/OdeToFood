using Microsoft.AspNet.Identity;
using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OdoToFood.Web.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {

        private OdeToFoodDbContext db;
        private IMenuData menuDb;
        private IMenuItemData menuItemDb;
        private IRestaurantData restaurantDb;
        private IRestaurantReviewData reviewDb;
        public ReviewsController(OdeToFoodDbContext db, IRestaurantData restaurantDb, IMenuData menuDb, IMenuItemData menuItemDb, IRestaurantReviewData reviewDb)
        {
            this.db = db;
            this.restaurantDb = restaurantDb;
            this.menuDb = menuDb;
            this.menuItemDb = menuItemDb;
            this.reviewDb = reviewDb;
        }

        [AllowAnonymous]
        // GET: Reviews
        public ActionResult Index(int restaurantId)
        {
            var model = reviewDb.GetAll(restaurantId);

            if (model.Count == 0)
            {
                return RedirectToAction("Create",new {restaurantId = restaurantId});
            }
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantReview restaurantReview, int restaurantId)
        {
            if (ModelState.IsValid)
            {
                restaurantReview.UserId = User.Identity.GetUserId();
                restaurantReview.RestaurantId = restaurantId;
                reviewDb.AddReview(restaurantReview);
            }
            if (User.IsInRole("RestaurantOwner, Admin"))
                return RedirectToAction("Details", "Restaurants", new { id = restaurantReview.RestaurantId });
            else
                return RedirectToAction("Index", new { restaurantId = restaurantId });
        }

        [HttpGet]
        public ActionResult GetUnapprovedReviews()
        {
            var model = reviewDb.GetAllUnapprovedReviews();
            if (model == null) 
                return View("AllReviewsAreApproved");
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = reviewDb.Get(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            reviewDb.DeleteReview(id);
            return RedirectToAction("GetUnapprovedReviews");
        }

        [HttpGet]
        public ActionResult Approve(int id)
        {
            var model = reviewDb.Get(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int id, FormCollection form)
        {
            reviewDb.ApproveReview(id);
            return RedirectToAction("GetUnapprovedReviews");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = reviewDb.Get(id);
            var userId = User.Identity.GetUserId();

            if (model == null || model.UserId != userId )
                return View("NotFound");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RestaurantReview restaurantReview)
        {
            var currentReview = reviewDb.Get(restaurantReview.Id);
            if (ModelState.IsValid)
            {
                reviewDb.EditReview(restaurantReview);
                if(!User.IsInRole("User"))
                    return RedirectToAction("Details","Restaurants", new { id = currentReview.RestaurantId });
                else
                    return RedirectToAction("Index",new { restaurantId = currentReview.RestaurantId });
            }
            return View();
        }

    }
}