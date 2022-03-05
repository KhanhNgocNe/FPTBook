namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "username" });
            CreateIndex("dbo.Orders", "Username");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "Username" });
            CreateIndex("dbo.Orders", "username");
        }
    }
}
