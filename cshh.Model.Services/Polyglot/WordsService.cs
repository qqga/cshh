using cshh.Data.Polyglot;
using cshh.Data.Services.Repo;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services.Polyglot
{
    public interface IWordsService //: IRepoContainer
    {
        IPolyglotRepository Repository { get; set; }

        IQueryable<Word> GetWordsStartsWith(string term);
        IEnumerable<Word> GetTranslates(int id);
        void Edit(Word word, string appUserId);
        void AddTranslate(Word word, int userWord_Id);
        void DeleteTranslate(int word_Id, int userWord_Id);
    }
    public class WordsService : IWordsService
    {
        IWorkContext _workContext;
        public WordsService(IPolyglotRepository repository, IWorkContext workContext)
        {
            Repository = repository;
            _workContext = workContext;
        }
        public IPolyglotRepository Repository { get; set; }

        public void AddWord(Word word)
        {
            Word wordNew = new Word();
            wordNew.Language = word.Language;
            wordNew.Text = word.Text;
            wordNew.Type = word.Type;
        }

        public IQueryable<Word> GetWordsStartsWith(string term)
        {
            return Repository.FindBy<Word>(w => w.Text.StartsWith(term));
        }

        public IEnumerable<Word> GetTranslates(int id)
        {
            Word byKey = Repository.GetByKey<UserWord>(id, "Word.TranslationsTo.Language", "Word.TranslationsFrom.Language", "Word.TranslationsTo.Type", "Word.TranslationsTo.Type").Word;
            return byKey.TranslationsTo.Union(byKey.TranslationsFrom);
        }
        public void Edit(Word word, string appUserId)
        {
            Word origin = this.Repository.GetByKey<Word>(word.Id, w => w.UserProfile);
            if(origin.UserProfile.ApplicationUserId != appUserId)
                throw new Exception("Попытка изменить чужие данные");

            origin.Type_Id = word.UserProfile_Id;
            origin.Language_Id = word.Language_Id;
            origin.Text = word.Text;
            origin.Type_Id = word.Type_Id;


            Repository.Update(origin, true);
        }

        public void AddTranslate(Word word, int userWord_Id)
        {
            UserProfile userProfile = Repository.FindBy<UserProfile>(up => up.ApplicationUserId == _workContext.UserAppId).FirstOrDefault();
            if(userProfile == null)
                throw new Exception("Пользователь не найден.");

            UserWord userWord = Repository.GetByKey<UserWord>(userWord_Id, uw => uw.Word);

            var wordRes = Repository.Words.FindSameWordOrDefault(word, out bool isfound);
            if(!isfound)
            {
                wordRes.UserProfile_Id = userProfile.Id;
                wordRes.Text = word.Text.ToLower();
                wordRes.Language_Id = word.Language_Id;
                wordRes.Type_Id = word.Type_Id;
                wordRes.DateTimeCreate = DateTime.Now;
            }

            userWord.Word.TranslationsTo.Add(wordRes);

            Repository.Update(userWord.Word, true);
        }

        public void DeleteTranslate(int word_Id, int userWord_Id)
        {
            Word word = Repository.FindBy<Word>(w => w.UserProfile.ApplicationUserId == _workContext.UserAppId && w.Id == word_Id).FirstOrDefault();

            if(word == null)
                throw new Exception("Слово не принадлежит пользователю.");

            UserWord userWord = Repository.GetByKey<UserWord>(userWord_Id, uw => uw.Word.TranslationsTo, uw => uw.Word.TranslationsFrom);

            if(userWord.Word.TranslationsFrom.Contains(word))
                userWord.Word.TranslationsFrom.Remove(word);

            if(userWord.Word.TranslationsTo.Contains(word))
                userWord.Word.TranslationsTo.Remove(word);

            Repository.Update(userWord.Word,true);

        }
    }
}
