using Data.RequestModels.ArticleModels;
using Data.RequestModels.CommanModels;
using Data.ReturnModels.ArticleModels;
using Data.ReturnModels.CommanModels;
using Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class ArticleService : IArticleService
    {

        #region Article CRUD & Search
        public ReturnModel ArticleAdd(AddArticleRequestModel Model)
        {
            throw new NotImplementedException();
        }

        public ParameterReturnModel<ArticleReturnModel> ArticleDetail(IdRequestModel Model)
        {
            throw new NotImplementedException();
        }

        public ParameterReturnModel<List<ArticleReturnModel>> ArticleList()
        {
            throw new NotImplementedException();
        }

        public ParameterReturnModel<List<ArticleReturnModel>> ArticleSearch(SearchRequestModel Model)
        {
            throw new NotImplementedException();
        }

        public ReturnModel ArticleUpdate(UpdateArticleRequestModel Model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
