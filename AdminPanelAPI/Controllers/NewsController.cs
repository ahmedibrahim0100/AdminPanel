using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AdminPanelAPI.Models;
using AdminPanelAPI.Models.DataModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.IO;

namespace AdminPanelAPI.Controllers
{
    public class NewsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NewsIdentityModels
        public IQueryable<NewsIdentityModel> GetNewsIdentities()
        {
            return db.NewsIdentities;
        }

        // GET: api/NewsIdentityModels/5
        [ResponseType(typeof(NewsIdentityModel))]
        public IHttpActionResult GetNewsIdentityModel(int id)
        {
            NewsIdentityModel newsIdentityModel = db.NewsIdentities.Find(id);
            if (newsIdentityModel == null)
            {
                return NotFound();
            }

            return Ok(newsIdentityModel);
        }

        // PUT: api/NewsIdentityModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewsIdentityModel(int id, NewsIdentityModel newsIdentityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsIdentityModel.Id)
            {
                return BadRequest();
            }

            db.Entry(newsIdentityModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsIdentityModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/NewsIdentityModels
        [ResponseType(typeof(NewsIdentityModel))]
        public IHttpActionResult PostNewsIdentityModel(NewsIdentityModel newsIdentityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NewsIdentities.Add(newsIdentityModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newsIdentityModel.Id }, newsIdentityModel);
        }

        [HttpPost]
        [Route("api/postarticle")]
        public IHttpActionResult PostArticle([FromBody]PostedArticleModel postedArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    NewsIdentityModel article = new NewsIdentityModel()
                    {
                        AuthorId = postedArticle.AuthorId,
                        NewsCategoryId = postedArticle.CategoryId,
                        CreationDate = DateTime.Now,
                        Title = postedArticle.Title,
                    };
                    db.NewsIdentities.Add(article);
                    db.SaveChanges();

                    NewsContentModel articleContent = new NewsContentModel()
                    {
                        NewsIdentityId = db.NewsIdentities.Last().Id,
                        Headline = postedArticle.Headline,
                        Body = postedArticle.Body
                    };
                    db.NewsContents.Add(articleContent);
                    db.SaveChanges();

                    List<ImageModel> articleImages = new List<ImageModel>();

                    HttpFileCollection uploadedImages = HttpContext.Current.Request.Files;
                    foreach (HttpPostedFile img in uploadedImages)
                    {
                        string imageUniqueName = Guid.NewGuid().ToString().Replace("-", "");

                        var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/Images"), imageUniqueName);
                        img.SaveAs(path);

                        ImageModel uploadedImage = new ImageModel()
                        {
                            ImageUniqueName = imageUniqueName,
                            ImageOriginalName = Path.GetFileName(img.FileName)
                        };

                        db.Images.Add(uploadedImage);
                        db.SaveChanges();

                        NewsImagesModel articleImage = new NewsImagesModel()
                        {
                            NewsIdentityId = db.NewsIdentities.Last().Id,
                            ImageId = db.Images.Last().Id
                        };
                        db.NewsImages.Add(articleImage);
                        db.SaveChanges();

                    }
                    

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                
            }
           
            return Ok();
        }

        // DELETE: api/NewsIdentityModels/5
        [ResponseType(typeof(NewsIdentityModel))]
        public IHttpActionResult DeleteNewsIdentityModel(int id)
        {
            NewsIdentityModel newsIdentityModel = db.NewsIdentities.Find(id);
            if (newsIdentityModel == null)
            {
                return NotFound();
            }

            db.NewsIdentities.Remove(newsIdentityModel);
            db.SaveChanges();

            return Ok(newsIdentityModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsIdentityModelExists(int id)
        {
            return db.NewsIdentities.Count(e => e.Id == id) > 0;
        }
    }
}