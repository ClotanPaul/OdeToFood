namespace OdeToFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdToStringInReviewsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RestaurantReviews", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RestaurantReviews", "UserId", c => c.Int(nullable: false));
        }
    }
}
