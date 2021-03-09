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

namespace AdminPanelAPI.Controllers
{
    public class NewsContentModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NewsContentModels
        public IQueryable<NewsContentModel> GetNewsContents()
        {
            return db.NewsContents;
        }

        // GET: api/NewsContentModels/5
        [ResponseType(typeof(NewsContentModel))]
        public IHttpActionResult GetNewsContentModel(int id)
        {
            NewsContentModel newsContentModel = db.NewsContents.Find(id);
            if (newsContentModel == null)
            {
                return NotFound();
            }

            return Ok(newsContentModel);
        }

        // PUT: api/NewsContentModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewsContentModel(int id, NewsContentModel newsContentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsContentModel.Id)
            {
                return BadRequest();
            }

            db.Entry(newsContentModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsContentModelExists(id))
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

        // POST: api/NewsContentModels
        [ResponseType(typeof(NewsContentModel))]
        public IHttpActionResult PostNewsContentModel(NewsContentModel newsContentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NewsContents.Add(newsContentModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newsContentModel.Id }, newsContentModel);
        }

        // DELETE: api/NewsContentModels/5
        [ResponseType(typeof(NewsContentModel))]
        public IHttpActionResult DeleteNewsContentModel(int id)
        {
            NewsContentModel newsContentModel = db.NewsContents.Find(id);
            if (newsContentModel == null)
            {
                return NotFound();
            }

            db.NewsContents.Remove(newsContentModel);
            db.SaveChanges();

            return Ok(newsContentModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsContentModelExists(int id)
        {
            return db.NewsContents.Count(e => e.Id == id) > 0;
        }
    }
}