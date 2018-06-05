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
using Newtonsoft.Json;
using System.Linq.Dynamic;
using AutoMapper;
using cshh.Asp.Models.Common;

namespace cshh.Asp.Areas.Polyglot.Controllers
{
    [Authorize]
    public class ForeignTextController : BasePolyglotController
    {
        IPolyglotListsService _polyglotListsService;
        IForeignTextService _foreignTexServise;
        IMapper _Mapper;
        IUserWordsService _userWordsService;
        public ForeignTextController(IForeignTextService foreignTextServies, IPolyglotListsService polyglotListsService, IUserWordsService userWordsService, IMapper mapper)
        {
            _foreignTexServise = foreignTextServies;
            _Mapper = mapper;
            _polyglotListsService = polyglotListsService;
            _userWordsService = userWordsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JqGridEditList()
        {
            ForeignTextListstViewModel foreignTextListstViewModel = new ForeignTextListstViewModel
            {
                Languages = _polyglotListsService.GetLanguaches().ToSelectListStr(t => t.Id, t => t.Name)
            };

            return PartialView("_JqGrid", foreignTextListstViewModel);
        }


        public ActionResult JqGridList(JqGridRequest jqGridRequest)
        {
            try
            {
                IQueryable<ForeignText> texts = _foreignTexServise.GetUserTexts().Include(t => t.Language);

                var resp = new JqGridResponse<ForeignText, ForeignTextViewModel>
                    (jqGridRequest, texts, w => w.Id, JsonConvert.DeserializeObject, w => _Mapper.Map<ForeignTextViewModel>(w));

                resp.rows.Count();//invoke query to detect errrors

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
        }

        public string JqGridAdd(ForeignTextViewModel viewModel)
        {
            try
            {
                ForeignText model = _Mapper.Map<ForeignText>(viewModel);
                _foreignTexServise.Add(model);
            }
            catch(Exception ex)
            {
                return this.JqGridBadRequest(ex);
            }

            return null;
        }
        public string JqGridEdit(ForeignTextViewModel viewModel)
        {
            try
            {
                ForeignText model = _Mapper.Map<ForeignText>(viewModel);
                _foreignTexServise.Edit(model);
            }
            catch(Exception ex)
            {
                return this.JqGridBadRequest(ex);
            }

            return null;
        }

        public string JqGridDelete(int id)
        {
            try
            {
                _foreignTexServise.Delete(id);
            }
            catch(Exception ex)
            {
                return this.JqGridBadRequest(ex);
            }

            return null;
        }
        [AllowAnonymous]
        public ActionResult ParseWords(int? foreignText_Id)
        {

            ParseWordsViewModel viewModel = new ParseWordsViewModel();
            viewModel.SeparatorsArr = _foreignTexServise.GetDefaultSeparators();
            if(foreignText_Id.HasValue)
            {
                ForeignText text = _foreignTexServise.GetUserTexts().First(t => t.Id == foreignText_Id.Value);
                viewModel.Text = text.Text;
            }
            //FillListsParseWordsVM(ref viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ParseWords(ParseWordsViewModel parseWordsViewModel)
        {
            var parsedWordsViewModel = new ParsedWordsViewModel();
            try
            {
                parsedWordsViewModel.Languages = _polyglotListsService.GetLanguaches().ToSeletListItems(l => l.Name, l => l.Id, 1);
                parsedWordsViewModel.Statuses = _polyglotListsService.GetWordStatuses().ToSeletListItems(s => s.Name, s => s.Id, 1);
                parsedWordsViewModel.Sets = _userWordsService.GetUserWordsSets(this.GetAppUserId()).Select(s => s.Name).ToList();
                parsedWordsViewModel.SetName = parsedWordsViewModel.Sets.FirstOrDefault();
                IEnumerable<WordInfo> parsedWords = _foreignTexServise.ParseWords(parseWordsViewModel.Text, parseWordsViewModel.SeparatorsArr);

                if(parseWordsViewModel.ExcludeExists)
                {
                    var existsWords = _userWordsService.GetUserWords().Select(w => w.Word.Text).ToList();
                    parsedWords = parsedWords.Where(w => !existsWords.Contains(w.Word));
                }

                parsedWordsViewModel.WordsJson = JsonConvert.SerializeObject(parsedWords.Select(w => new { w.Word, w.Count, id = Guid.NewGuid() }));

                //FillListsParseWordsVM(ref parseWordsViewModel);
            }
            catch(Exception ex)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.OK, ex.CollectMessages());
                ModelState.AddModelError("", ex.CollectMessages());
            }
            return PartialView("_ParsedWords", parsedWordsViewModel);
        }

        //void FillListsParseWordsVM(ref ParseWordsViewModel viewModel)
        //{
        //        viewModel.Sets = _userWordsService.GetUserWordsSets(this.GetAppUserId()).Select(s => s.Name);
        //        viewModel.Languages = _polyglotListsService.GetLanguaches().ToSeletListItems(l => l.Name, l => l.Id, viewModel.Language_Id);
        //    
        //}

        [HttpPost]
        public ActionResult SaveParsedWords(ParsedWordsViewModel parseWordsViewModel)
        {
            int wordsCount = 0;
            try
            {                
                var deserializeObject = JsonConvert.DeserializeObject<WordInfo[]>(parseWordsViewModel.WordsJson);
                var words = deserializeObject.Select(w => w.Word);
                _userWordsService.Add(words, parseWordsViewModel.SetName, parseWordsViewModel.Language_Id, parseWordsViewModel.Status_Id);
                wordsCount = words.Count();
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }

            return Json(wordsCount);
            //return RedirectToAction("Index", "Home");
        }


        public ActionResult Read(int id)
        {
            ForeignText text = _foreignTexServise.GetTexts().Where(t => t.Id == id).Include(t => t.Bookmarks).First();

            ForeignTextReadViewModel viewModel = _Mapper.Map<ForeignTextReadViewModel>(text);

            return View(viewModel);
        }

        [HttpPost]

        public ActionResult AddBookmark(Bookmark bookmark)
        {
            //Bookmark bookmark = new Bookmark()
            //{
            //    ForeignText_Id = textId,
            //    Note = note,
            //    Position = position,
            //    Title = title
            //};

            try
            {
                var newBookmark = _foreignTexServise.AddBookMark(bookmark);
                return Json(_Mapper.Map<BookmarkViewModel>(newBookmark), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }



            //return Redirect(Url.Action("Read", new { id = bookmark.ForeignText_Id }) + "#bookmark-" + bookmark.Position);
        }
        [HttpPost]

        public ActionResult DeleteBookmark(int id)
        {
            try
            {
                _foreignTexServise.DeleteBookmark(id);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
            return null;
        }
        [HttpPost]

        public ActionResult EditBookmark(Bookmark bookmark)
        {
            try
            {
                var bookmarkRes = _foreignTexServise.EditBookmark(bookmark);
                return Json(_Mapper.Map<BookmarkViewModel>(bookmarkRes), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
        }
    }
}