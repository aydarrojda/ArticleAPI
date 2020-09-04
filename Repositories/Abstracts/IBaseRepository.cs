using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Abstracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region CRUD
        TEntity Add(TEntity Model);

        TEntity Get(int Id);

        List<TEntity> List();

        void Delete(TEntity Model);

        void Update(TEntity Model);

        #endregion
    }
}
