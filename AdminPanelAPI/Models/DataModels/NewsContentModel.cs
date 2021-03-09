using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    public class NewsContentModel
    {
        [Key]
        public int Id { get; set; }
        public int NewsIdentityId { get; set; }
        public string Headline { get; set; }
        public string Body { get; set; }

        [ForeignKey("NewsIdentityId")]
        public NewsIdentityModel NewsIdentity { get; set; }
    }
}