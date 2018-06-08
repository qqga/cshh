using cshh.Asp.Models.Polyglot;
using cshh.Data.Polyglot;
using cshh.Model.Services.Polyglot;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cshh.Asp.Extensions;
using Common.Asp.JqGrid;

using System.Linq.Expressions;
using cshh.Data.Services.Repo;
using AutoMapper;

//todo permisssion service
namespace cshh.Asp.Areas.Polyglot.Controllers
{
    public class WordController : BasePolyglotController
    {
        IWordsService _wordsService;
        IMapper _mapper;
        public WordController(IWordsService wordsService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _wordsService = wordsService;
        }
        public JsonResult WordsLike(string term)
        {
            var words = _wordsService.GetWordsStartsWith(term).Select(w => new { value = w.Text, id = w.Id }).ToList();

            //return Json(new[] { new { value = "asdf", id = 1 }, new { value = "ZXc", id = 2 } }, JsonRequestBehavior.AllowGet);
            return Json(words, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JqGridTranslations(JqGridRequest jqGridRequest, int userWord_Id)
        {
            try
            {
                IEnumerable<Word> translates = _wordsService.GetTranslates(userWord_Id);

                var resp = new JqGridResponse<Word, WordViewModel>
                    (jqGridRequest, translates.AsQueryable(), w => w.Id, Newtonsoft.Json.JsonConvert.DeserializeObject, w => /*new WordViewModel(w)*/_mapper.Map<WordViewModel>(w));

                resp.rows.Count();//invoke query to detect errrors

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }

        }

        [Authorize]
        public string JqGridEdit(WordViewModel wordVM)
        {
            try
            {
                //Word word = wordVM.ToModel();
                Word word = _mapper.Map<Word>(wordVM);
                _wordsService.Edit(word, this.GetAppUserId());
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }
        [Authorize]
        public string JqGridAddTranslate(WordViewModel wordVM, int userWord_Id)
        {
            try
            {
                //Word word = wordVM.ToModel();
                Word word = _mapper.Map<Word>(wordVM);
                _wordsService.AddTranslate(word, userWord_Id);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }
        [Authorize]
        public string JqGridDeleteTranslate(int id, int userWord_Id)
        {
            try
            {
                _wordsService.DeleteTranslate(id, userWord_Id);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }

    }


}