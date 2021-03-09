namespace AdminPanelAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminPanelAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdminPanelAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Authors.AddOrUpdate(DummyData.SeedAuthors().ToArray());
            //context.SaveChanges();

            //context.NewsCategories.AddOrUpdate(DummyData.SeedNewsCategories().ToArray());
            //context.SaveChanges();

            //context.NewsIdentities.AddOrUpdate(DummyData.SeedNewsIdentities().ToArray());
            //context.SaveChanges();

            //context.NewsContents.AddOrUpdate(DummyData.SeedNewsContents().ToArray());
            //context.SaveChanges();

            //context.StructureSections.AddOrUpdate(DummyData.SeedStructureSections().ToArray());
            //context.SaveChanges();

            //context.NewsPositions.AddOrUpdate(DummyData.SeedNewsPositions().ToArray());
            //context.SaveChanges();
        }
    }
}
