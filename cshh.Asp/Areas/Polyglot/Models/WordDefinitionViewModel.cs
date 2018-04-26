using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Polyglot
{
    public class WordDefinitionViewModel
    {
        public WordDefinitionViewModel()
        {
        }
        public int Id { get; set; }
        public int Word_Id { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
        public bool Public { get; set; }
        public int? Type_Id { get; set; }
        public string TypeName { get; set; }

        //public WordDefinition ToModel()
        //{
        //    return new WordDefinition()
        //    {

        //        Id = Id,
        //        Word_Id  = Word_Id,
        //        Definition = Definition,
        //        Example = Example,
        //        Public = Public,
        //        WordType_Id = WordType_Id
        //    };
        //}

        //public WordDefinitionViewModel(WordDefinition wordDefinition)
        //{
        //    Id = wordDefinition.Id;
        //    Word_Id = wordDefinition.Word_Id;
        //    Definition = wordDefinition.Definition;
        //    Example = wordDefinition.Example;
        //    Public = wordDefinition.Public;
        //    WordType_Id = wordDefinition.WordType_Id;
        //    WordType = wordDefinition.WordType.Name;

        //}
    }
}