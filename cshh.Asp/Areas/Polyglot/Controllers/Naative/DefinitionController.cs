using cshh.Asp.Extensions;
using cshh.Asp.Models.Polyglot;
using cshh.Data.Polyglot;
using cshh.Data.User;
using cshh.Model.Services.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Areas.Polyglot.Controllers
{

    public class DefinitionController : BasePolyglotController
    {
        IWordDefinitionService _wordDefinitionService;
        IPolyglotListsService _polyglotListsService;
        public DefinitionController(IWordDefinitionService wordDefinitionService, IPolyglotListsService polyglotListsService) : base()
        {
            _wordDefinitionService = wordDefinitionService;
            _polyglotListsService = polyglotListsService;
        }

        [AllowAnonymous]
        public ActionResult List(int wordId)
        {
            IEnumerable<WordDefinition> wordDefinitions = _wordDefinitionService.GetWordDefinitions(wordId);
            UserProfile userProfile = this.GetUserProfile(_wordDefinitionService.Repository); //todo решить вопрос. repo спрятать от контроллера
            bool isAdminRole = this.User?.IsInRole(Roles.AdminRole) ?? false;
            IEnumerable<DefinitionViewModel> select = wordDefinitions
                .Select(d => new DefinitionViewModel(d)
                { Editable = (d.UserProfile.ApplicationUserId == userProfile?.ApplicationUserId) || isAdminRole });

            var wordDefinitionViewModel = new DefinitionListViewModel() { Definitions = select };
            return PartialView("_List", wordDefinitionViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //todo 
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "can't delete for some reasons1!");

            //if(!(this.User.IsInRole(Roles.AdminRole) || _wordDefinitionService.IsUserWordDefenition(id, this.GetAppUserId())))
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Текущий пользователь не может удалить это описание");
            //else
            //    _wordDefinitionService.Remove(id);

            //return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult EditPartial(int id)
        {
            WordDefinition wordDefinition = _wordDefinitionService.GetWordDefinition(id);
            var vm = new DefinitionEditViewModel( /*wordDefinition */) { Definition = "asd" };//todo 
            vm.WordTypes = GetWordTypes(wordDefinition.Id);

            return PartialView("_Edit");
        }

        [HttpPost]
        public ActionResult EditPartial(DefinitionEditViewModel definition)
        {
            WordDefinition model = definition.ToModel();
            _wordDefinitionService.Edit(ref model);
            return PartialView("_ListItem", new DefinitionViewModel(model));
        }

        IEnumerable<SelectListItem> GetWordTypes(int selectedDefenitionId = -1)
        {
            return _polyglotListsService.GetWordTypes()
                 .ToSeletListItems(t => t.Name, t => t.Id, t => t.Id == selectedDefenitionId);
        }

    }
}