using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    //Represents the relationship between articles and their images

    public class NewsImagesModel
    {
        [Key]
        public int Id { get; set; }
        public int NewsIdentityId { get; set; }
        public int ImageId { get; set; }

        [ForeignKey("NewsIdentityId")]
        public NewsIdentityModel NewsIdentity { get; set; }

        [ForeignKey("ImageId")]
        public ImageModel Image { get; set; }
    }
}