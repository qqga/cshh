using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace cshh.Asp.Models.Polyglot
{
    public class WordEditViewModel
    {

        [DisplayName("Слово")]
        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        [DisplayName("Набор")]
        [Required]
        public WordSet WordSet { get; set; }
        public IEnumerable<WordSet> WordSets { get; set; }// allow new

        [DisplayName("Cтатус")]
        [Required]
        public WordStatus Status { get; set; }
        public IEnumerable<WordStatus> Statuses { get; set; }

        [DisplayName("Язык")]
        [Required]
        public Language Language { get; set; }
        public IEnumerable<Language> Languages { get; set; }

        [DisplayName("Тип")]
        [Required]
        public WordType WordType { get; set; }
        public IEnumerable<WordType> WordTypes { get; set; }

        public IEnumerable<WordDefinition> Defenitions { get; set; } // edit list
        public IEnumerable<Word> Translations { get; set; } //edit list


        //public Word ToNewWord()
        //{
        //    return new Word()
        //    {
        //        Text = this.Text?.Trim().ToLower(),
        //        Language_Id = this.Language.Id,
        //        Type_Id = this.WordType.Id
        //    };
        //}

        //public UserWord ToNewUserWord()
        //{

        //    return new UserWord()
        //    {
        //        Set = this.WordSet,
        //        Set_Id = this.WordSet.Id,
        //        Status_Id = this.Status.Id,                 
        //        Word = ToNewWord()
        //    };
        //}
    }
}
