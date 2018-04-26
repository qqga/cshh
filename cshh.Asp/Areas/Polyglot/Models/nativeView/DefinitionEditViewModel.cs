using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Models.Polyglot
{
    public class DefinitionEditViewModel
    {
        public DefinitionEditViewModel()
        {
        }
        public DefinitionEditViewModel(WordDefinition wordDefinition)
        {
            this.Id = wordDefinition.Id;
            this.Definition = wordDefinition.Definition;
            this.Example = wordDefinition.Example;
            this.Public = wordDefinition.Public;
            this.WordType = wordDefinition.Type;
        }
        //public UserProfile UserProfile { get; set; }
        //public Word Word { get; set; }

        public WordDefinition ToModel()
        {
            return new WordDefinition()
            {
                Definition = this.Definition,
                Id = this.Id,
                Example = this.Example,
                Public = this.Public,
                Type = this.WordType
            };
        }

        [DisplayName("DefinitionId")]
        public int Id { get; set; }
        [DisplayName("Описание")]
        public string Definition { get; set; }
        [DisplayName("Примеры")]
        public string Example { get; set; }
        [DisplayName("Тип")]
        public WordType WordType { get; set; }
        [DisplayName("Публичный")]
        public bool Public { get; set; }

        public IEnumerable<SelectListItem> WordTypes { get; set; }
    }
}