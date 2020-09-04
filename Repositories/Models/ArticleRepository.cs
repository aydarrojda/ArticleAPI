using Data.Entities;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Models
{
    public class ArticleRepository : BaseRepository<Articles>, IArticleRepository
    {
        private readonly ArticleDBContext _context;
        public ArticleRepository(ArticleDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
