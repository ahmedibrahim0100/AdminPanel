namespace AdminPanelAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyImagesTableAndAddNewsImagesRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewsImagesModels", "NewsIdentityId", "dbo.NewsIdentityModels");
            DropIndex("dbo.NewsImagesModels", new[] { "NewsIdentityId" });
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsIdentityId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageModels", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.NewsIdentityModels", t => t.NewsIdentityId, cascadeDelete: true)
                .Index(t => t.NewsIdentityId)
                .Index(t => t.ImageId);
            
            AddColumn("dbo.NewsIdentityModels", "CreationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.NewsIdentityModels", "PublishingDate");
            DropTable("dbo.NewsImagesModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NewsImagesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsIdentityId = c.Int(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.NewsIdentityModels", "PublishingDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.NewsImages", "NewsIdentityId", "dbo.NewsIdentityModels");
            DropForeignKey("dbo.NewsImages", "ImageId", "dbo.ImageModels");
            DropIndex("dbo.NewsImages", new[] { "ImageId" });
            DropIndex("dbo.NewsImages", new[] { "NewsIdentityId" });
            DropColumn("dbo.NewsIdentityModels", "CreationDate");
            DropTable("dbo.NewsImages");
            DropTable("dbo.ImageModels");
            CreateIndex("dbo.NewsImagesModels", "NewsIdentityId");
            AddForeignKey("dbo.NewsImagesModels", "NewsIdentityId", "dbo.NewsIdentityModels", "Id", cascadeDelete: true);
        }
    }
}
