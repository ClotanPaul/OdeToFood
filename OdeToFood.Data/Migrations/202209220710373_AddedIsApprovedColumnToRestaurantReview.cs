namespace OdeToFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsApprovedColumnToRestaurantReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantReviews", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantReviews", "IsApproved");
        }
    }
}
