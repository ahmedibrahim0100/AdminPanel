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
    public class StructureSectionModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StructureSectionModels
        public IQueryable<StructureSectionModel> GetStructureSections()
        {
            return db.StructureSections;
        }

        // GET: api/StructureSectionModels/5
        [ResponseType(typeof(StructureSectionModel))]
        public IHttpActionResult GetStructureSectionModel(int id)
        {
            StructureSectionModel structureSectionModel = db.StructureSections.Find(id);
            if (structureSectionModel == null)
            {
                return NotFound();
            }

            return Ok(structureSectionModel);
        }

        // PUT: api/StructureSectionModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStructureSectionModel(int id, StructureSectionModel structureSectionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != structureSectionModel.Id)
            {
                return BadRequest();
            }

            db.Entry(structureSectionModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StructureSectionModelExists(id))
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

        // POST: api/StructureSectionModels
        [ResponseType(typeof(StructureSectionModel))]
        public IHttpActionResult PostStructureSectionModel(StructureSectionModel structureSectionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StructureSections.Add(structureSectionModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = structureSectionModel.Id }, structureSectionModel);
        }

        // DELETE: api/StructureSectionModels/5
        [ResponseType(typeof(StructureSectionModel))]
        public IHttpActionResult DeleteStructureSectionModel(int id)
        {
            StructureSectionModel structureSectionModel = db.StructureSections.Find(id);
            if (structureSectionModel == null)
            {
                return NotFound();
            }

            db.StructureSections.Remove(structureSectionModel);
            db.SaveChanges();

            return Ok(structureSectionModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StructureSectionModelExists(int id)
        {
            return db.StructureSections.Count(e => e.Id == id) > 0;
        }
    }
}