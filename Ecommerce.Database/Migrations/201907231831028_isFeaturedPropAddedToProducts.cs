namespace Ecommerce.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isFeaturedPropAddedToProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "isFeatured");
        }
    }
}
