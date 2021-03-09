namespace AdminPanelAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNewsImagesTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NewsImages", newName: "NewsImagesModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.NewsImagesModels", newName: "NewsImages");
        }
    }
}
