using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    //Correlates the article to the part of website where it should be displayed. For example:-
    //the article with the Id: 1 (NewsId = 1) is to be displayed at HomePage (which has StructureSectionId: 1)
    //but now i think there is no need for this class at all 

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