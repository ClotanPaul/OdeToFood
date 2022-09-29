using OdeToFood.Data.Models;
using System.Data.Entity;

namespace OdeToFood.Data.Services
{
    public class OdeToFoodDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<RestaurantReview> RestaurantReviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<RestaurantMenu>()
                .HasRequired<Restaurant>(s => s.Restaurant)
                .WithMany(g => g.Menus)
                .HasForeignKey<int>(s => s.RestaurantId);
        }
    }

}
