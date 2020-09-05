using Data.Entities;
using Data.RequestModels.ArticleModels;
using Data.RequestModels.CommanModels;
using Data.ReturnModels.ArticleModels;
using Data.ReturnModels.CommanModels;
using Data.SystemModels;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Options;
using Repositories.Abstracts;
using Services.Abstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace Services.Models
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ImageModel _imageModel;

        public ArticleService(IArticleRepository articleRepository,IOptions<ImageModel> options)
        {
            _articleRepository = articleRepository;
            _imageModel = options.Value;

        }
        #region Article CRUD & Search
        public ReturnModel ArticleAdd(AddArticleRequestModel Model, ImageUploadRequestModel ImageModel)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("", "");
                }

                if (Model.Title == null)
                {
                    return new ReturnModel("", "");
                }

                if (Model.Content == null)
                {
                    return new ReturnModel("", "");
                }

                string Image = null;
                if (Model.File != null)
                {
                    if (Model.File.Length > 0)
                    {
                        if (ImageModel.WwwRoot == null)
                        {
                            return new ReturnModel("", "");
                        }

                        if (_imageModel.FolderName != null)
                        {
                            return new ReturnModel("", "");
                        }

                        if (!Model.File.ContentType.Contains("Image"))
                        {
                            return new ReturnModel("", "");
                        }

                        string ImageName = $"{Guid.NewGuid().ToString().Substring(5)}.{Model.File.ContentType.Split('/')[1]}";

                        string FolderPath = Path.Combine(ImageModel.WwwRoot, _imageModel.FolderName, ImageName);

                        using (FileStream stream = File.Create(FolderPath))
                        {
                            Model.File.CopyTo(stream);

                        }

                        if (!File.Exists(FolderPath))
                        {
                            return new ReturnModel("", "");
                        }

                        Image = FolderPath;
                    }
                }

                if (Model.Author == null)
                {
                    return new ReturnModel("", "");
                }

                var ArticleModel = new Articles()
                {
                    Title = Model.Title,
                    Content = Model.Content,
                    Author = Model.Author,
                    Image = Image,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

               var GetRecord= _articleRepository.Add(ArticleModel);

                if (GetRecord.Id == 0)
                {
                    return new ReturnModel("", "");
                }

                return new ReturnModel();
            }
            catch (Exception Ex)
            {
                return new ReturnModel("", "");
            }
        }

        public ParameterReturnModel<ArticleReturnModel> ArticleDetail(IdRequestModel Model)
        {
            try
            {
                if (Model.Id == 0)
                {
                    return new ParameterReturnModel<ArticleReturnModel>("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "YTOOSHEJ89");
                }

                var Article = _articleRepository.Get(Model.Id);

                if (Article == null)
                {
                    return new ParameterReturnModel<ArticleReturnModel>("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "BJSUYX6NQD");
                }

                var Result = new ArticleReturnModel()
                {
                    Id = Article.Id,
                    Title = Article.Title,
                    Content = Article.Content,
                    Author = Article.Author,
                    Image = Article.Image
                };

                return new ParameterReturnModel<ArticleReturnModel>(Result);
            }
            catch (Exception Ex)
            {
                return new ParameterReturnModel<ArticleReturnModel>($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "B0O4FH2RBX");
            }
        }

        public ParameterReturnModel<List<ArticleReturnModel>> ArticleList()
        {
            try
            {
                var ArticleList = _articleRepository.List();

                var Result = new List<ArticleReturnModel>();
                foreach (var article in ArticleList)
                {
                    var ArticleModel = new ArticleReturnModel()
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Content = article.Content,
                        Author = article.Author,
                        Image = article.Image
                    };

                    Result.Add(ArticleModel);
                }

                return new ParameterReturnModel<List<ArticleReturnModel>>(Result);
            }
            catch (Exception Ex)
            {

                return new ParameterReturnModel<List<ArticleReturnModel>>($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "OUP0L68SD6");
            }
        }

        public ParameterReturnModel<List<ArticleReturnModel>> ArticleSearch(SearchRequestModel Model)
        {
            try
            {
                var ArticleList=_articleRepository.List();

                var SearchResult=ArticleList.Where(x => x.Title.ToLower().Contains(Model.Keyword.ToLower()) || x.Content.Contains(Model.Keyword.ToLower()) || x.Author.ToLower().Contains(Model.Keyword.ToLower())).ToList();

                var Result = new List<ArticleReturnModel>();
                foreach (var search in SearchResult)
                {
                    var ArticleModel = new ArticleReturnModel()
                    {
                        Id = search.Id,
                        Title = search.Title,
                        Content = search.Content,
                        Author = search.Author,
                        Image= search.Image
                    };

                    Result.Add(ArticleModel);
                }

                return new ParameterReturnModel<List<ArticleReturnModel>>(Result);
            }
            catch (Exception Ex)
            {

                return new ParameterReturnModel<List<ArticleReturnModel>>("", "");
            }
        }

        public ReturnModel ArticleUpdate(UpdateArticleRequestModel Model, ImageUploadRequestModel ImageModel)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("", "");
                }

                if (Model.Id == 0)
                {
                    return new ReturnModel("", "");
                }

                var GetArticle = _articleRepository.Get(Model.Id);

                if (Model.Title == null)
                {
                    return new ReturnModel("", "");
                }

                if (Model.Content == null)
                {
                    return new ReturnModel("", "");
                }


                if (Model.File != null)
                {
                    if (Model.File.Length > 0)
                    {
                        if (ImageModel.WwwRoot == null)
                        {
                            return new ReturnModel("", "");
                        }

                        if (_imageModel.FolderName != null)
                        {
                            return new ReturnModel("", "");
                        }

                        if (!Model.File.ContentType.Contains("Image"))
                        {
                            return new ReturnModel("", "");
                        }

                        string ImageName = $"{Guid.NewGuid().ToString().Substring(5)}.{Model.File.ContentType.Split('/')[1]}";

                        string FolderPath = Path.Combine(ImageModel.WwwRoot, _imageModel.FolderName, ImageName);

                        using (FileStream stream = File.Create(FolderPath))
                        {
                            Model.File.CopyTo(stream);

                        }

                        if (!File.Exists(FolderPath))
                        {
                            return new ReturnModel("", "");
                        }

                        GetArticle.Image = FolderPath;
                    }
                }

                if (Model.Author == null)
                {
                    return new ReturnModel("", "");
                }

                GetArticle.Title = Model.Title;
                GetArticle.Content = Model.Content;
                GetArticle.Author = Model.Author;
                GetArticle.ModifiedDate = DateTime.Now;

                _articleRepository.Update(GetArticle);

                return new ReturnModel();
            }
            catch (Exception Ex)
            {
                return new ReturnModel("", "");
            }
        }
        #endregion
    }
}
