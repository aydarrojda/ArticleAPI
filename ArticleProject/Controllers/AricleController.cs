using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.RequestModels.ArticleModels;
using Data.RequestModels.CommanModels;
using Data.ReturnModels.ArticleModels;
using Data.ReturnModels.CommanModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstracts;
using Services.Abstracts;

namespace ArticleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AricleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AricleController(IArticleService articleService, IWebHostEnvironment webHostEnvironment)
        {
            _articleService = articleService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/Article/Add")]
        [HttpPost]
        public ReturnModel Add(AddArticleRequestModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "YDGTEFL1L0");
                }
                var Result = _articleService.ArticleAdd(Model,new ImageUploadRequestModel() { WwwRoot= _webHostEnvironment.WebRootPath });

                if (Result.Success == false)
                {
                    return new ReturnModel(Result.ErrorMessage,Result.ErrorCode);
                }
                return new ReturnModel();
            }
            catch (Exception Ex)
            {
                return new ReturnModel($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "OP7S32GBPY");
            }
        }


        [Route("/Article/Detail/{Id}")]
        [HttpGet]
        public ParameterReturnModel<ArticleReturnModel> Detail(IdRequestModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return new ParameterReturnModel<ArticleReturnModel>("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "FTF0FUQGEQ");
                }

                var Result=_articleService.ArticleDetail(Model);

                if (Result.Success == false)
                {
                    return new ParameterReturnModel<ArticleReturnModel>(Result.ErrorMessage,Result.ErrorCode);
                }
                return new ParameterReturnModel<ArticleReturnModel>(Result.Model);
            }
            catch (Exception Ex)
            {
                return new ParameterReturnModel<ArticleReturnModel>($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "RNY276N5VQ");
            }
        }

        [Route("/Article/List")]
        [HttpGet]
        public ParameterReturnModel<List<ArticleReturnModel>> List()
        {
            try
            {
                var Result = _articleService.ArticleList();

                if (Result.Success == false)
                {
                    return new ParameterReturnModel<List<ArticleReturnModel>>(Result.ErrorMessage,Result.ErrorCode);
                }
                return new ParameterReturnModel<List<ArticleReturnModel>>(Result.Model);
            }
            catch (Exception Ex)
            {
                return new ParameterReturnModel<List<ArticleReturnModel>>($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "YJVEFDQAMQ");
            }
        }

        [Route("/Article/Update")]
        [HttpPost]
        public ReturnModel Update(UpdateArticleRequestModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "");
                }
                var Result = _articleService.ArticleUpdate(Model, new ImageUploadRequestModel() { WwwRoot = _webHostEnvironment.WebRootPath });

                if (Result.Success == false)
                {
                    return new ReturnModel(Result.ErrorMessage, Result.ErrorCode);
                }
                return new ReturnModel();
            }
            catch (Exception Ex)
            {
                return new ReturnModel($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "");
            }
        }

        [Route("/Article/Search")]
        [HttpPost]
        public ReturnModel Search(SearchRequestModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return new ReturnModel("Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi.", "");
                }
                var Result = _articleService.ArticleSearch(Model);

                if (Result.Success == false)
                {
                    return new ReturnModel(Result.ErrorMessage, Result.ErrorCode);
                }
                return new ReturnModel();
            }
            catch (Exception Ex)
            {
                return new ReturnModel($"Teknik bir hata meydana geldiğinden işlem gerçekleştirilemedi. Hata detayı : {Ex.Message}", "");
            }
        }
    }
}
