namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        AnnouncementId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 32),
                        Description = c.String(nullable: false, maxLength: 512),
                        CategoryId = c.Int(nullable: false),
                        SubcategoryId = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.AnnouncementId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Subcategories", t => t.SubcategoryId)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.CategoryId)
                .Index(t => t.SubcategoryId)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        HeadingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Headings", t => t.HeadingId)
                .Index(t => t.HeadingId);
            
            CreateTable(
                "dbo.Headings",
                c => new
                    {
                        HeadingId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.HeadingId);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        SubcategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubcategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.AnnouncementTags",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        AnnouncementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagId, t.AnnouncementId })
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.Announcements", t => t.AnnouncementId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.AnnouncementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Announcements", "Username", "dbo.Users");
            DropForeignKey("dbo.AnnouncementTags", "AnnouncementId", "dbo.Announcements");
            DropForeignKey("dbo.AnnouncementTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Announcements", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Announcements", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "HeadingId", "dbo.Headings");
            DropIndex("dbo.AnnouncementTags", new[] { "AnnouncementId" });
            DropIndex("dbo.AnnouncementTags", new[] { "TagId" });
            DropIndex("dbo.Subcategories", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "HeadingId" });
            DropIndex("dbo.Announcements", new[] { "Username" });
            DropIndex("dbo.Announcements", new[] { "SubcategoryId" });
            DropIndex("dbo.Announcements", new[] { "CategoryId" });
            DropTable("dbo.AnnouncementTags");
            DropTable("dbo.Users");
            DropTable("dbo.Tags");
            DropTable("dbo.Subcategories");
            DropTable("dbo.Headings");
            DropTable("dbo.Categories");
            DropTable("dbo.Announcements");
        }
    }
}
