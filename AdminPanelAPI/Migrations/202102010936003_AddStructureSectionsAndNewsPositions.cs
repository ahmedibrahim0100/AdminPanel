namespace AdminPanelAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStructureSectionsAndNewsPositions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsPositionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        StructureSectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsIdentityModels", t => t.NewsId, cascadeDelete: true)
                .ForeignKey("dbo.StructureSectionModels", t => t.StructureSectionId, cascadeDelete: true)
                .Index(t => t.NewsId)
                .Index(t => t.StructureSectionId);
            
            CreateTable(
                "dbo.StructureSectionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsPositionModels", "StructureSectionId", "dbo.StructureSectionModels");
            DropForeignKey("dbo.NewsPositionModels", "NewsId", "dbo.NewsIdentityModels");
            DropIndex("dbo.NewsPositionModels", new[] { "StructureSectionId" });
            DropIndex("dbo.NewsPositionModels", new[] { "NewsId" });
            DropTable("dbo.StructureSectionModels");
            DropTable("dbo.NewsPositionModels");
        }
    }
}
