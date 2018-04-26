using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Polyglot
{
    public class UserWordJqViewModel
    {
        public int Id { get; set; }

        public string DateTimeCreate { get; set; }

        public int Word_Id { get; set; }
        public string WordText { get; set; }

        public int WordLanguage_Id { get; set; }
        public string WordLanguageName { get; set; }
        public string WordLanguageShortName { get; set; }

        public string SetName { get; set; }
        public int Set_Id { get; set; }

        public string StatusName { get; set; }
        public int Status_Id { get; set; }

        public string WordTypeName { get; set; }
        public int? WordType_Id { get; set; }
        public string Translations { get; set; }

        public UserWordJqViewModel()
        {
        }

        //public UserWordJqViewModel(UserWord userWord)
        //{
        //    this.Id = userWord.Id;

        //    this.Word_Id = userWord.Word_Id;
        //    this.WordText = userWord.Word.Text;

        //    this.DateTimeCreate = userWord.DateTimeCreate.ToString("dd.MM.yy HH:mm");

        //    this.SetName = userWord.Set.Name;
        //    this.Set_Id = userWord.Set_Id;

        //    this.StatusName = userWord.Status.Name;
        //    this.Status_Id = userWord.Status_Id;

        //    this.WordLanguage_Id = userWord.Word.Language_Id;
        //    this.WordLanguageName = userWord.Word.Language.Name;
        //    this.WordLanguageShortName = userWord.Word.Language.ShortName;

        //    this.WordTypeName = userWord.Word.Type?.Name;
        //    this.WordType_Id = userWord.Word.Type_Id;

        //    this.Translations = string.Join(/*"; "*/Environment.NewLine,
        //        userWord.Word.TranslationsFrom
        //        .Union(userWord.Word.TranslationsTo)
        //        .GroupBy(t => t.Language.ShortName)
        //        .Select(gr => $"{gr.Key}: {string.Join(", ", gr.Select(w => w.Text)) }"));
        //}
        //public UserWord ToModel()
        //{

        //    return new UserWord()
        //    {
        //        Id = this.Id,
        //        Set_Id = this.Set_Id,
        //        Status_Id = this.Status_Id,
        //        //Status = new WordStatus { Id = this.Status_Id },

        //        Word_Id = Word_Id,
        //        Word = new Word()
        //        {
        //            Id = Word_Id,
        //            Text = this.WordText?.Trim().ToLower(),
        //            Language_Id = this.WordLanguage_Id,
        //            //Language = new Data.Polyglot.Language() { Id = this.Language_Id },
        //            Type_Id = this.WordType_Id,
        //            //WordType = new WordType() { Id = this.Type_Id.Value },
        //        },

        //    };
        //}

    }

    //public class UserWordsListViewModel
    //{
    //    public UserWordsListViewModel(IEnumerable<UserWord> userWords)
    //    {
    //        UserWords = userWords.Select(uw => new UserWordViewModel(uw));
    //    }
    //    public IEnumerable<UserWordViewModel> UserWords { get; set; }

    //}

    //public class UserWordViewModel
    //{
    //    public int Id { get; set; }
    //    public string Set { get; set; }
    //    public string Status { get; set; }
    //    public string Language { get; set; }
    //    public string Text { get; set; }
    //    public string Type { get; set; }
    //    
    //    public UserWordViewModel(UserWord userWord)
    //    {
    //        this.Id = userWord.Id;

    //        this.Set = userWord.Set.Name;
    //        this.Status = userWord.Status.Name;

    //        this.Language = userWord.Word.Language.Name;
    //        this.Text = userWord.Word.Text;
    //        this.Type = userWord.Word.WordType.Name;

    //    }

    //}


}