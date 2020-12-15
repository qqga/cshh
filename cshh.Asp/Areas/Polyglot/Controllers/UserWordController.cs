using AutoMapper;
using Common.Asp.JqGrid;
using cshh.Asp.Extensions;
using cshh.Asp.Models.Common;
using cshh.Asp.Models.Polyglot;
using cshh.Data.Polyglot;
using cshh.Data.Services.Repo;
using cshh.Model.Services.Polyglot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
//using System.Linq.Dynamic;

namespace cshh.Asp.Areas.Polyglot.Controllers
{
    public class UserWordController : BasePolyglotController
    {
        IUserWordsService _wordsService;
        IMapper _mapper;
        IPolyglotListsService _PolyglotListsService;
        public UserWordController(IUserWordsService wordsService, IPolyglotListsService polyglotListsService, IMapper mapper) : base()
        {
            _wordsService = wordsService;
            _mapper = mapper;
            _PolyglotListsService = polyglotListsService;
        }

        IQueryable<UserWord> UserWordsQuery =>
            _wordsService.GetUserWords(this.GetAppUserId())
            .Include(uw => uw.Status)
            .Include(uw => uw.Set)
            .Include(uw => uw.Word.Language)
            .Include(uw => uw.Word.Type)
            .Include("Word.TranslationsTo.Language")
            .Include("Word.TranslationsFrom.Language");


        public ActionResult Index()
        {
            return View();
        }

        #region jqGrid

        public ActionResult JqGridJsonList(JqGridRequest jqGridRequest)
        {
            try
            {
                _wordsService.Repository.SetProxyCreationEnabled(false);

                var jqGridResponse = new JqGridResponse<UserWord, UserWordJqViewModel>
                    (jqGridRequest, UserWordsQuery, uw => uw.Id, JsonConvert.DeserializeObject, /*uw => new UserWordJqViewModel(uw)*/ uw => _mapper.Map<UserWordJqViewModel>(uw));

                JsonResult json = Json(jqGridResponse, JsonRequestBehavior.AllowGet);
                return json;

            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
        }

        [Authorize]
        public ActionResult JqGridEditList()
        {
            var jqGrid_WordEditLists = new JqGrid_WordEditLists()
            {
                LanguagesStr = _PolyglotListsService.GetLanguaches().ToSelectListStr(t => t.Id, t => t.Name),
                Statuses = _PolyglotListsService.GetWordStatuses().ToSeletListItems(s => s.Name, s => s.Id, 1),
                StatusesStr = _PolyglotListsService.GetWordStatuses().ToSelectListStr(t => t.Id, t => t.Name),
                WordTypesStr = _PolyglotListsService.GetWordTypes().ToSelectListStr(t => t.Id, t => t.Name),
                WordSets = _wordsService.GetUserWordsSets(this.GetAppUserId()).Select(t => t.Name),
                WordSetsStr = _wordsService.GetUserWordsSets(this.GetAppUserId()).ToSelectListStr(t => t.Id, t => t.Name),
            };

            return PartialView("_JqGrid", jqGrid_WordEditLists);
        }

        [HttpPost]
        [Authorize]
        public string JqGridAdd(UserWordJqViewModel editWordVM)
        {
            try
            {
                UserWord map = _mapper.Map<UserWord>(editWordVM);

                _wordsService.Add(map);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }

        [Authorize]
        [HttpPost]
        public string JqGridEdit(UserWordJqViewModel userWordVM)
        {
            try
            {
                UserWord map = _mapper.Map<UserWord>(userWordVM);

                _wordsService.Edit(map);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }

        [Authorize]
        [HttpPost]
        public string JqGridDelete(string id)
        {
            try
            {
                foreach(var item in id.Split(','))
                {
                    int intId = Convert.ToInt32(item);
                    _wordsService.Delete(intId);
                }
                //
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }
            return null;
        }

        [Authorize]
        public string ChangeWordsStatus(int status_Id, int[] words)
        {
            try
            {
                _wordsService.ChangeWordsStatus(status_Id, words);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return "Ok";
        }

        #endregion


        #region _List
        //[HttpGet]
        ////[Authorize]
        //public ActionResult Add()
        //{
        //    var editWordViewModel = new WordEditViewModel()
        //    {
        //        Languages = _wordsService.GetLanguaches().ToList(),
        //        Statuses = _wordsService.GetWordStatuses().ToList(),
        //        WordTypes = _wordsService.GetWordTypes().ToList(),
        //        WordSets = _wordsService.GetUserWordsSets(this.GetAppUserId()),
        //    };

        //    return PartialView("_Add", editWordViewModel);
        //}

        //[Authorize]
        //public ActionResult ListUserWordsPartial()
        //{
        //    UserWordsListViewModel viewModel = new UserWordsListViewModel(UserWordsQuery);
        //    return PartialView("_List", viewModel);
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Add(WordEditViewModel editWordViewModel)
        //{
        //    if(!ModelState.IsValid)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, this.ModelState.CollectErrors());

        //    UserWordViewModel newUserWordViewModel = null;

        //    try
        //    {
        //        UserWord model = editWordViewModel.ToNewUserWord();

        //        UserWord newUserWord = _wordsService.Add(model);

        //        newUserWord = _wordsService.GetUserWords().Where(uw => uw.Id == newUserWord.Id)
        //            .Include(w => w.Status)
        //            .Include(w => w.Word.Language)
        //            .Include(w => w.Word.WordType)
        //            .First();

        //        newUserWordViewModel = new UserWordViewModel(newUserWord);
        //    }
        //    catch(Exception ex)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
        //    }
        //    return PartialView("_ListItem", newUserWordViewModel);
        //}

        //[Authorize]
        //public ActionResult Edit(int id)
        //{

        //    _wordsService.GetUserWords().Where(uw => uw.Id == id).FirstOrDefault(); ;

        //    var editWordViewModel = new WordEditViewModel()
        //    {
        //        Languages = _wordsService.GetLanguaches().ToList(),
        //        Statuses = _wordsService.GetWordStatuses().ToList(),
        //        WordTypes = _wordsService.GetWordTypes().ToList(),
        //        WordSets = _wordsService.GetUserWordsSets(this.GetAppUserId()),
        //    };

        //    return PartialView("_Add", editWordViewModel);
        //}




        ////[Authorize]
        ////public ActionResult Edit(WordEditViewModel id)
        ////{

        ////}

        //[Authorize]
        //public ActionResult DeleteUserWord(int id)
        //{
        //    try
        //    {
        //        _wordsService.Delete(id);
        //    }
        //    catch(Exception ex)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
        //    }

        //    return new HttpStatusCodeResult(HttpStatusCode.OK);
        //}

        #endregion

        public ActionResult AddWordExt(string word, string userKey, string defaultSetName, int? defaultStatusId)
        {
            if(string.IsNullOrWhiteSpace(userKey))
                throw new Exception("word o userKey not set");

            word = word.Trim();
            var vm = new AddWordExtViewModel()
            {
                UserKey = userKey,
                Sets = _wordsService.GetUserWordsSets(userKey).Select(s => s.Name).ToList(),
                WordText = word,
                Languages = _PolyglotListsService.GetLanguaches().ToSeletListItems(l => l.Name, l => l.Id, 1),
                Statuses = _PolyglotListsService.GetWordStatuses().ToSeletListItems(s => s.Name, s => s.Id, 1),
                UrlPost = Request.Url.GetLeftPart(UriPartial.Path),
                Status_Id = defaultStatusId ?? 1,
                TranslateLanguage_Id = 2//todo magic to extension params
            };
            var existWordInfo = _wordsService.GetWordTranslates(word, userKey);
            if(existWordInfo != null)
                vm.ExistWordInfo = "Слово есть вашем словаре: " + existWordInfo;

            vm.SetName = !string.IsNullOrWhiteSpace(defaultSetName) ? defaultSetName : vm.Sets.First();

            return View(vm);

        }

        [HttpPost]
        public ActionResult AddWordExt(AddWordExtViewModel userWordVM)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            try
            {
                UserWord userWord = _mapper.Map<UserWord>(userWordVM);

                _wordsService.Add(userWord, userWordVM.Example, userWordVM.TranslateLanguage_Id, userWordVM.Translates, userWordVM.UserKey);
            }
            catch(Exception ex)
            {
                //return Json(ex.CollectMessages());
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.CollectMessages());
            }
            return Json("Ok");
        }

        public class JqGrid_WordEditLists //todo
        {
            //public IEnumerable<WordSet> WordSets { get; set; }
            //public IEnumerable<WordStatus> Statuses { get; set; }
            //public IEnumerable<Language> Languages { get; set; }
            //public IEnumerable<WordType> WordTypes { get; set; }

            public IEnumerable<string> WordSets { get; set; }
            public string WordSetsStr { get; set; }//=> string.Join("; ", WordSets.Select(ws => $"{ws.Id}:{ws.Name}"));
            public IEnumerable<SelectListItem> Statuses { get; set; }
            public string StatusesStr { get; set; }//=> string.Join("; ", Statuses.Select(s => $"{s.Id}:{s.Name}"));
            public string LanguagesStr { get; set; }// => string.Join("; ", Languages.Select(l => $"{l.Id}:{l.Name}"));
            public string WordTypesStr { get; set; }// => string.Join("; ", WordTypes.Select(wt => $"{wt.Id}:{wt.Name}"));

        }
    }
}
