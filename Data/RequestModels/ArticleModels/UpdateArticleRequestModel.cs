using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.RequestModels.ArticleModels
{
    public class UpdateArticleRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile File { get; set; }
        public string Author { get; set; }
    }
}
