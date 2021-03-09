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
using System.Web;
using System.IO;

namespace AdminPanelAPI.Controllers
{
    public class ImageModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ImageModels
        public IQueryable<ImageModel> GetImages()
        {
            return db.Images;
        }

        // GET: api/ImageModels/5
        [ResponseType(typeof(ImageModel))]
        public IHttpActionResult GetImageModel(int id)
        {
            ImageModel imageModel = db.Images.Find(id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return Ok(imageModel);
        }

        // PUT: api/ImageModels/5
        [ResponseType(typeof(void))]
        [Route("api/updateimage")]
        public IHttpActionResult PutImageModel(ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(imageModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageModelExists(imageModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(imageModel);
        }

        // POST: api/ImageModels
        //[HttpPost]
        //[Route("api/postimage")]
        //public IHttpActionResult PostImageModel([FromBody]string imageLink)
        //{
        //ImageModel image = new ImageModel() { ImageLink = imageLink };
        //db.Images.Add(image);
        //db.SaveChanges();

        //return Ok(image);

        //return CreatedAtRoute("DefaultApi", new { id = imageModel.Id }, imageModel);
        //}

        [HttpPost]
        [Route("api/uploadimage")]
        //public IHttpActionResult UploadImage(HttpPostedFileBase img)
        public IHttpActionResult UploadImage()
        {
            HttpFileCollection images = HttpContext.Current.Request.Files;
            List<int> imagesIdsList = new List<int>();

            foreach (HttpPostedFile img in images)
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

                imagesIdsList.Add(db.Images.Last().Id);
            }
      
            return Ok(imagesIdsList);
        }

        // DELETE: api/ImageModels/5
        [ResponseType(typeof(ImageModel))]
        public IHttpActionResult DeleteImageModel(int id)
        {
            ImageModel imageModel = db.Images.Find(id);
            if (imageModel == null)
            {
                return NotFound();
            }

            db.Images.Remove(imageModel);
            db.SaveChanges();

            return Ok(imageModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageModelExists(int id)
        {
            return db.Images.Count(e => e.Id == id) > 0;
        }
    }
}