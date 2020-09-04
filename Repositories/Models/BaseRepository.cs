using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Models
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        #region DI
        private readonly ArticleDBContext _context;
        private readonly DbSet<TEntity> _db;
        #endregion

        #region Constructor
        public BaseRepository(ArticleDBContext context)
        {
            _context = context;
            _db = _context.Set<TEntity>();
        }
        #endregion

        #region CRUD
        public TEntity Add(TEntity Model)
        {
            _db.Add(Model);
            _context.SaveChanges();
            return Model;
        }

        public void Delete(TEntity Model)
        {
            _db.Remove(Model);
            _context.SaveChanges();

        }

        public TEntity Get(int Id)
        {
            TEntity Model = _db.Find(Id);
            return Model;
        }

        public List<TEntity> List()
        {
            List<TEntity> ListModel = _db.ToList();
            return ListModel;
        }

        public void Update(TEntity Model)
        {
            _db.Update(Model);
            _context.SaveChanges();
        }

        #endregion
    }
}
