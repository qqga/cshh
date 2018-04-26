using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Polyglot
{
    public class WordViewModel
    {
        public WordViewModel()
        {
        }
        //public WordViewModel(Word word)
        //{

        //    this.Id = word.Id;
        //    this.Text = word.Text;
        //    this.Language = word.Language.Name;
        //    this.Language_Id = word.Language_Id;
        //    this.Type = word.WordType?.Name;
        //    this.Type_Id = word.WordType_Id;

        //}

        public int Id { get; set; }
        public string Text { get; set; }
        public string LanguageName { get; set; }
        public int Language_Id { get; set; }
        public string TypeName { get; set; }
        public int? Type_Id { get; set; }

        //public Word ToModel()
        //{
        //    return new Word()
        //    {
        //        Id = this.Id,
        //        Text = this.Text,
        //        Language_Id = this.Language_Id,
        //        WordType_Id = this.Type_Id
        //    };
        //}
    }
}