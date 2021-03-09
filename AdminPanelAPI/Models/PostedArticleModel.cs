using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelAPI.Models
{
    public class PostedArticleModel
    {
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Body { get; set; }
        //public int[] ImagesIdsList { get; set; }
    }
}