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
using AutoMapper;

namespace cshh.Asp.Areas.Polyglot.Controllers
{
    public class WordDefinitionController : BasePolyglotController
    {
        IWordDefinitionService _wordDefinitionService;
        IMapper _Mapper;
        public WordDefinitionController(IWordDefinitionService wordDefinitionService, IMapper mapper)
        {
            _wordDefinitionService = wordDefinitionService;
            _Mapper = mapper;
        }

        public ActionResult JqGridList(JqGridRequest jqGridRequest, int word_Id)
        {
            try
            {
                var translates = _wordDefinitionService.GetWordDefinitions(word_Id);

                var resp = new JqGridResponse<WordDefinition, WordDefinitionViewModel>
                    (jqGridRequest, translates.AsQueryable(), d => d.Id, Newtonsoft.Json.JsonConvert.DeserializeObject, d => /*new WordDefinitionViewModel(d)*/ _Mapper.Map<WordDefinitionViewModel>(d));

                resp.rows.Count();//invoke query to detect errrors before procees actionResult

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
        }

        public string JqGridEdit(WordDefinitionViewModel definitionVM)
        {
            try
            {
                var model = _Mapper.Map<WordDefinition>(definitionVM);
                _wordDefinitionService.Edit(ref model);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }
            return null;
        }

        public string JqGridAdd(WordDefinitionViewModel definitionVM)
        {
            try
            {
                //WordDefinition model = definitionVM.ToModel();
                var model = _Mapper.Map<WordDefinition>(definitionVM);
                _wordDefinitionService.Add(model);

            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }
            return null;
        }

        public string JqGridDelete(int id, int word_id)
        {
            try
            {
                _wordDefinitionService.Delete(id);
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }
            return null;
        }
    }
}