using AdminPanelAPI.Models;
using AdminPanelAPI.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Helpers
{
    public class DummyData
    {
        public static List<AuthorModel> SeedAuthors()
        {
            List<AuthorModel> authors = new List<AuthorModel>()
            {
                new AuthorModel() { Id = 1, Name = "Anonymous1" },
                new AuthorModel() { Id = 2, Name = "Anonymous2" }
            };

            return authors;
        }

        public static List<NewsCategoryModel> SeedNewsCategories()
        {
            List<NewsCategoryModel> newsCategories = new List<NewsCategoryModel>()
            {
                new NewsCategoryModel() { Id = 1, Name = "Category1" },
                new NewsCategoryModel() { Id = 2, Name = "Category2" }
            };

            return newsCategories;
        }

        public static List<NewsIdentityModel> SeedNewsIdentities()
        {
            List<NewsIdentityModel> news = new List<NewsIdentityModel>()
            {
                new NewsIdentityModel() { Id = 1, Title = "First Article", AuthorId = 1, NewsCategoryId = 1, CreationDate = DateTime.Now },
                new NewsIdentityModel() { Id = 2, Title = "Second Article", AuthorId = 2 , NewsCategoryId = 2, CreationDate = DateTime.Now},
                new NewsIdentityModel()  { Id = 3, Title = "Third Article", AuthorId = 1 , NewsCategoryId = 2, CreationDate = DateTime.Now}
            };

            return news;
        }

        public static List<NewsContentModel> SeedNewsContents()
        {
            List<NewsContentModel> newsContents = new List<NewsContentModel>()
            {
                new NewsContentModel() { Id = 1, Headline = "First Headline", Body = "BlaBlaBlaBla", NewsIdentityId = 1 },
                new NewsContentModel() { Id = 2, Headline = "Second Headline", Body = "BoooBoooBoooBoooBooo", NewsIdentityId = 2 },
                new NewsContentModel() { Id = 3, Headline = "Third Headline", Body = "Aaaaaaaaaaaaaaaa", NewsIdentityId = 3 }
            };

            return newsContents;
        }

        public static List<StructureSectionModel> SeedStructureSections()
        {
            List<StructureSectionModel> structureSections = new List<StructureSectionModel>()
            {
                new StructureSectionModel() { Id = 1, Name = "Home Page" }
            };

            return structureSections;
        }

        public static List<NewsPositionModel> SeedNewsPositions()
        {
            List<NewsPositionModel> newsPositions = new List<NewsPositionModel>()
            {
                new NewsPositionModel() { Id = 1, NewsId = 1, StructureSectionId = 1 }
            };

            return newsPositions;
        }
    }
}