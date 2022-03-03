namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "addressOrder", c => c.String());
            AddColumn("dbo.Orders", "phoneOrders", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "phoneOrders");
            DropColumn("dbo.Orders", "addressOrder");
        }
    }
}
