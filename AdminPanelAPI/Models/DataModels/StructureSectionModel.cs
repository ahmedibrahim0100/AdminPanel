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
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}