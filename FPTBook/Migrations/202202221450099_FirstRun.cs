namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        bookID = c.Int(nullable: false, identity: true),
                        bookName = c.String(nullable: false),
                        description = c.String(),
                        stock_quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        categoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.bookID)
                .ForeignKey("dbo.Categories", t => t.categoryID, cascadeDelete: true)
                .Index(t => t.categoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryID = c.Int(nullable: false, identity: true),
                        categoryName = c.String(nullable: false),
                        description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.categoryID);
            
            CreateTable(
                "dbo.OrdersDetails",
                c => new
                    {
                        ordersID = c.Int(nullable: false),
                        bookID = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        quantity = c.Int(nullable: false),
                        amount = c.Double(nullable: false),
                        Order_orderID = c.Int(),
                    })
                .PrimaryKey(t => new { t.ordersID, t.bookID })
                .ForeignKey("dbo.Books", t => t.bookID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_orderID)
                .Index(t => t.bookID)
                .Index(t => t.Order_orderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderID = c.Int(nullable: false, identity: true),
                        username = c.String(maxLength: 128),
                        orderDate = c.DateTime(nullable: false),
                        total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.orderID)
                .ForeignKey("dbo.Users", t => t.username)
                .Index(t => t.username);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 128),
                        password = c.String(nullable: false),
                        confirmPassword = c.String(nullable: false),
                        fullname = c.String(nullable: false),
                        telephone = c.Int(nullable: false),
                        email = c.String(nullable: false),
                        address = c.String(nullable: false, maxLength: 200),
                        gender = c.String(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        state = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "username", "dbo.Users");
            DropForeignKey("dbo.OrdersDetails", "Order_orderID", "dbo.Orders");
            DropForeignKey("dbo.OrdersDetails", "bookID", "dbo.Books");
            DropForeignKey("dbo.Books", "categoryID", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "username" });
            DropIndex("dbo.OrdersDetails", new[] { "Order_orderID" });
            DropIndex("dbo.OrdersDetails", new[] { "bookID" });
            DropIndex("dbo.Books", new[] { "categoryID" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OrdersDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
