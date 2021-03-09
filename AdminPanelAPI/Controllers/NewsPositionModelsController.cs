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
    public class NewsPositionModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NewsPositionModels
        public IQueryable<NewsPositionModel> GetNewsPositions()
        {
            return db.NewsPositions;
        }

        // GET: api/NewsPositionModels/5
        [ResponseType(typeof(NewsPositionModel))]
        public IHttpActionResult GetNewsPositionModel(int id)
        {
            NewsPositionModel newsPositionModel = db.NewsPositions.Find(id);
            if (newsPositionModel == null)
            {
                return NotFound();
            }

            return Ok(newsPositionModel);
        }

        // PUT: api/NewsPositionModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewsPositionModel(int id, NewsPositionModel newsPositionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsPositionModel.Id)
            {
                return BadRequest();
            }

            db.Entry(newsPositionModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsPositionModelExists(id))
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

        // POST: api/NewsPositionModels
        [ResponseType(typeof(NewsPositionModel))]
        public IHttpActionResult PostNewsPositionModel(NewsPositionModel newsPositionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NewsPositions.Add(newsPositionModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newsPositionModel.Id }, newsPositionModel);
        }

        // DELETE: api/NewsPositionModels/5
        [ResponseType(typeof(NewsPositionModel))]
        public IHttpActionResult DeleteNewsPositionModel(int id)
        {
            NewsPositionModel newsPositionModel = db.NewsPositions.Find(id);
            if (newsPositionModel == null)
            {
                return NotFound();
            }

            db.NewsPositions.Remove(newsPositionModel);
            db.SaveChanges();

            return Ok(newsPositionModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsPositionModelExists(int id)
        {
            return db.NewsPositions.Count(e => e.Id == id) > 0;
        }
    }
}