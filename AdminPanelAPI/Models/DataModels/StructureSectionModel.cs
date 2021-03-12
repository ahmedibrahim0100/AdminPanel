using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    public class StructureSectionModel
    {
        //I think i don't need this class any more
        //It was to allow user to create corresponding sections for the website like: HomePage, ....
        //but I believe now it is wrong mindset

        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}