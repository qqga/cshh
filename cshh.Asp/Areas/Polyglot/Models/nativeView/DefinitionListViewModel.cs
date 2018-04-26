using cshh.Data.Polyglot;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Models.Polyglot
{
    public class DefinitionListViewModel
    {
        public IEnumerable<DefinitionViewModel> Definitions { get; set; }

        public IEnumerable<SelectListItem> WordTypes { get; set; }
    }

    public class DefinitionViewModel
    {
        public DefinitionViewModel()
        {
        }
        public DefinitionViewModel(WordDefinition wordDefinition)
        {
            this.Id = wordDefinition.Id;
            this.Definition = wordDefinition.Definition;
            this.Example = wordDefinition.Example;
            this.Public = wordDefinition.Public;
            this.WordType = wordDefinition.Type.Name;
        }
        //public UserProfile UserProfile { get; set; }
        //public Word Word { get; set; }

        [DisplayName("DefinitionId")]
        public int Id { get; set; }
        [DisplayName("Описание")]
        public string Definition { get; set; }
        [DisplayName("Примеры")]
        public string Example { get; set; }
        [DisplayName("Тип")]
        public string WordType { get; set; }
        [DisplayName("Публичный")]
        public bool Public { get; set; }

        public bool Editable { get; set; }
    }
}