using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models.DataModels
{
    //The main representative of an article entity

    public class NewsIdentityModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public int NewsCategoryId { get; set; }

        [ForeignKey("NewsCategoryId")]
        public NewsCategoryModel NewsCategory { get; set; }

        [ForeignKey("AuthorId")]
        public AuthorModel Author { get; set; }
    }
}