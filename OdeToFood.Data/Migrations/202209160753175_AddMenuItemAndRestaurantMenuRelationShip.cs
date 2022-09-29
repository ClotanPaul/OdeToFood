namespace OdeToFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuItemAndRestaurantMenuRelationShip : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItems", "RestaurantMenu_Id", "dbo.RestaurantMenus");
            DropIndex("dbo.MenuItems", new[] { "RestaurantMenu_Id" });
            RenameColumn(table: "dbo.MenuItems", name: "RestaurantMenu_Id", newName: "RestaurantMenuId");
            AlterColumn("dbo.MenuItems", "RestaurantMenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.MenuItems", "RestaurantMenuId");
            AddForeignKey("dbo.MenuItems", "RestaurantMenuId", "dbo.RestaurantMenus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItems", "RestaurantMenuId", "dbo.RestaurantMenus");
            DropIndex("dbo.MenuItems", new[] { "RestaurantMenuId" });
            AlterColumn("dbo.MenuItems", "RestaurantMenuId", c => c.Int());
            RenameColumn(table: "dbo.MenuItems", name: "RestaurantMenuId", newName: "RestaurantMenu_Id");
            CreateIndex("dbo.MenuItems", "RestaurantMenu_Id");
            AddForeignKey("dbo.MenuItems", "RestaurantMenu_Id", "dbo.RestaurantMenus", "Id");
        }
    }
}
