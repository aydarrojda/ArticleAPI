using Data.Entities;
using Data.RequestModels.ArticleModels;
using Data.RequestModels.CommanModels;
using Data.ReturnModels.ArticleModels;
using Data.ReturnModels.CommanModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Services.Abstracts
{
    public interface IArticleService
    {
        #region Article CRUD & Search
        ReturnModel ArticleAdd(AddArticleRequestModel Model);

        ParameterReturnModel<ArticleReturnModel> ArticleDetail(IdRequestModel Model);

        ParameterReturnModel<List<ArticleReturnModel>> ArticleList();

        ReturnModel ArticleUpdate(UpdateArticleRequestModel Model);

        ParameterReturnModel<List<ArticleReturnModel>> ArticleSearch(SearchRequestModel Model);
        #endregion
    }
}
