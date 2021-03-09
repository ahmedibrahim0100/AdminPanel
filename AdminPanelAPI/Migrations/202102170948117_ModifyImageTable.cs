namespace AdminPanelAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyImageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "ImageUniqueName", c => c.String());
            AddColumn("dbo.ImageModels", "ImageOriginalName", c => c.String());
            DropColumn("dbo.ImageModels", "ImageLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageModels", "ImageLink", c => c.String());
            DropColumn("dbo.ImageModels", "ImageOriginalName");
            DropColumn("dbo.ImageModels", "ImageUniqueName");
        }
    }
}
