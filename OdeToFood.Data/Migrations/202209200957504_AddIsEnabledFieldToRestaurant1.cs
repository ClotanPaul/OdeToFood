namespace OdeToFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsEnabledFieldToRestaurant1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "DeactivationReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "DeactivationReason");
        }
    }
}
