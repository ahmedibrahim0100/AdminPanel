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
    public class NewsCategoryModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NewsCategoryModels
        public IQueryable<NewsCategoryModel> GetNewsCategories()
        {
            return db.NewsCategories;
        }

        // GET: api/NewsCategoryModels/5
        [ResponseType(typeof(NewsCategoryModel))]
        public IHttpActionResult GetNewsCategoryModel(int id)
        {
            NewsCategoryModel newsCategoryModel = db.NewsCategories.Find(id);
            if (newsCategoryModel == null)
            {
                return NotFound();
            }

            return Ok(newsCategoryModel);
        }

        // PUT: api/NewsCategoryModels/5
        [ResponseType(typeof(void))]
        [Route("api/updatecategory")]
        public IHttpActionResult PutNewsCategoryModel(NewsCategoryModel newsCategoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(newsCategoryModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsCategoryModelExists(newsCategoryModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(newsCategoryModel);
        }

        // POST: api/NewsCategoryModels
        //[ResponseType(typeof(NewsCategoryModel))]
        [HttpPost]
        [Route("api/postcategory")]
        public IHttpActionResult PostNewsCategoryModel([FromBody]string categoryName)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            NewsCategoryModel category = new NewsCategoryModel() { Name = categoryName };
            db.NewsCategories.Add(category);
            db.SaveChanges();

            return Ok(category);
            //return CreatedAtRoute("DefaultApi", new { id = newsCategoryModel.Id }, newsCategoryModel);
        }

        // DELETE: api/NewsCategoryModels/5
        [ResponseType(typeof(NewsCategoryModel))]
        [Route("api/deletecategory")]
        public IHttpActionResult DeleteNewsCategoryModel([FromBody]int id)
        {
            NewsCategoryModel newsCategoryModel = db.NewsCategories.Find(id);
            if (newsCategoryModel == null)
            {
                return NotFound();
            }

            db.NewsCategories.Remove(newsCategoryModel);
            db.SaveChanges();

            return Ok(newsCategoryModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsCategoryModelExists(int id)
        {
            return db.NewsCategories.Count(e => e.Id == id) > 0;
        }
    }
}