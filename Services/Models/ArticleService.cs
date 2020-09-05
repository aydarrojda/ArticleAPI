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
        #region DI
        private readonly IArticleRepository _articleRepository;
        private readonly ImageModel _imageModel;
        #endregion

        #region Constructor
        public ArticleService(IArticleRepository articleRepository,IOptions<ImageModel> options)
        {
            _articleRepository = articleRepository;
            _imageModel = options.Value;

        }
        #endregion

        #region Article CRUD & Search
        public ReturnModel ArticleAdd(AddArticleRequestModel Model, ImageUploadRequestModel ImageModel)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("Hiç veri girilmediğinden işlem gerçekleştirilemedi.", "8Z46Q9UU4P");
                }

                if (Model.Title == null)
                {
                    return new ReturnModel("Başlık bilgisi girilmediğin işlem gerçekleştirilemedi.", "F4L8NW11AJ");
                }

                if (Model.Content == null)
                {
                    return new ReturnModel("İçerik bilgisi girilmediğinden işlem gerçekleştirilemedi.", "FUEST5ANL5");
                }

                string Image = null;
                if (Model.File != null)
                {
                    if (Model.File.Length > 0)
                    {
                        if (ImageModel.WwwRoot == null)
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "HFLMOE42BJ");
                        }

                        if (_imageModel.FolderName != null)
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "VP3TZHA2QS");
                        }

                        if (!Model.File.ContentType.Contains("Image"))
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "19C97GGDEY");
                        }

                        string ImageName = $"{Guid.NewGuid().ToString().Substring(5)}.{Model.File.ContentType.Split('/')[1]}";

                        string FolderPath = Path.Combine(ImageModel.WwwRoot, _imageModel.FolderName, ImageName);

                        using (FileStream stream = File.Create(FolderPath))
                        {
                            Model.File.CopyTo(stream);

                        }

                        if (!File.Exists(FolderPath))
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "A29Y5O3CI9");
                        }

                        Image = $"/{_imageModel.FolderName}/{ImageName}";
                    }
                }

                if (Model.Author == null)
                {
                    return new ReturnModel("Yazar bilgisi girilmediğinden işlem gerçekleştirilemedi.", "XQ99TJWRY8");
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
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "95D5NU0H2A");
                }

                return new ReturnModel();
            }
            catch (Exception Ex)
            {
                return new ReturnModel($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "SQ343ZCD1X");
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

                return new ParameterReturnModel<List<ArticleReturnModel>>($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "6H17CMHSR0");
            }
        }

        public ReturnModel ArticleUpdate(UpdateArticleRequestModel Model, ImageUploadRequestModel ImageModel)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("Hiç veri girilmediğinden işlem gerçekleştirilemedi.", "X72HJTSS55");
                }

                if (Model.Id == 0)
                {
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "UZEW5HEYAL");
                }

                var GetArticle = _articleRepository.Get(Model.Id);

                if (Model.Title == null)
                {
                    return new ReturnModel("Başlık bilgisi girilmediğin işlem gerçekleştirilemedi.", "");
                }

                if (Model.Content == null)
                {
                    return new ReturnModel("İçerik bilgisi girilmediğinden işlem gerçekleştirilemedi.", "II1JT41VT3");
                }


                if (Model.File != null)
                {
                    if (Model.File.Length > 0)
                    {
                        if (ImageModel.WwwRoot == null)
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "F9E3VPVYQ0");
                        }

                        if (_imageModel.FolderName != null)
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "V0N7W750F4");
                        }

                        if (!Model.File.ContentType.Contains("Image"))
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "AUVEHNXAJ5");
                        }

                        string ImageName = $"{Guid.NewGuid().ToString().Substring(5)}.{Model.File.ContentType.Split('/')[1]}";

                        string FolderPath = Path.Combine(ImageModel.WwwRoot, _imageModel.FolderName, ImageName);

                        using (FileStream stream = File.Create(FolderPath))
                        {
                            Model.File.CopyTo(stream);

                        }

                        if (!File.Exists(FolderPath))
                        {
                            return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "UK8VFUDUY8");
                        }

                        GetArticle.Image = $"/{_imageModel.FolderName}/{ImageName}";
                    }
                }

                if (Model.Author == null)
                {
                    return new ReturnModel("Yazar bilgisi girilmediğinden işlem gerçekleştirilemedi.", "1CWM90O6MZ");
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
                return new ReturnModel($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "BZFB8KIS88");
            }
        }

        public ReturnModel ArticleDelete(IdRequestModel Model)
        {
            try
            {
                if (Model.Id == 0)
                {
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "OAME1JY8Y7");
                }

                var Article = _articleRepository.Get(Model.Id);

                _articleRepository.Delete(Article);

                var IsDeleted = _articleRepository.Get(Model.Id);

                if (IsDeleted != null)
                {
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "7ZV77MF69A");
                }
                return new ReturnModel();
            }
            catch (Exception Ex)
            {

                return new ReturnModel($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "N74Z19ES71");
            }
        }
        #endregion
    }
}
