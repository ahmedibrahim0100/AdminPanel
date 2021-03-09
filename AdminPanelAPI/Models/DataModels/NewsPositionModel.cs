using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    public class NewsPositionModel
    {
        [Key]
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int StructureSectionId { get; set; }

        [ForeignKey("NewsId")]
        public NewsIdentityModel NewsIdentity { get; set; }

        [ForeignKey("StructureSectionId")]
        public StructureSectionModel StructureSection { get; set; }
    }
}