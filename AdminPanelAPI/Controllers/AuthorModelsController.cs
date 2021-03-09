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
    public class AuthorModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AuthorModels
        public IQueryable<AuthorModel> GetAuthors()
        {
            return db.Authors;
        }

        // GET: api/AuthorModels/5
        [ResponseType(typeof(AuthorModel))]
        public IHttpActionResult GetAuthorModel(int id)
        {
            AuthorModel authorModel = db.Authors.Find(id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return Ok(authorModel);
        }

        // PUT: api/AuthorModels/5
        [ResponseType(typeof(void))]
        [Route("api/updateauthor")]
        public IHttpActionResult PutAuthorModel(AuthorModel authorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Entry(authorModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorModelExists(authorModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(authorModel);
        }

        // POST: api/AuthorModels
        //[ResponseType(typeof(AuthorModel))]
        [HttpPost]
        [Route("api/postauthor")]
        public IHttpActionResult PostAuthorModel([FromBody]string authorName)
        {
           
            AuthorModel author = new AuthorModel() { Name = authorName };
            db.Authors.Add(author);
            db.SaveChanges();

            return Ok(author);
                      
            //return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        // DELETE: api/AuthorModels/5
        [ResponseType(typeof(AuthorModel))]
        [Route("api/deleteauthor")]
        public IHttpActionResult DeleteAuthorModel([FromBody]int id)
        {
            AuthorModel authorModel = db.Authors.Find(id);
            if (authorModel == null)
            {
                return NotFound();
            }

            db.Authors.Remove(authorModel);
            db.SaveChanges();

            return Ok(authorModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorModelExists(int id)
        {
            return db.Authors.Count(e => e.Id == id) > 0;
        }
    }
}