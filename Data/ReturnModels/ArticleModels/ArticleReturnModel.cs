using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ReturnModels.ArticleModels
{
    public class ArticleReturnModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
    }
}
