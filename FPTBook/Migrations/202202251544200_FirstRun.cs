namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Img", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Img", c => c.String());
        }
    }
}
