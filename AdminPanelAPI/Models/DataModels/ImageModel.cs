using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    //The main representative for images entities

    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string ImageUniqueName { get; set; }
        public string ImageOriginalName { get; set; }
    }
}