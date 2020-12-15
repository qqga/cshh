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
    public interface IUserWordsService
    {
        void Add(IEnumerable<string> words, string setName, int lang_id, int status_id);

        IPolyglotRepository Repository { get; set; }
        IEnumerable<WordSet> GetUserWordsSets(string userAppId);
        IQueryable<UserWord> GetUserWords(string userAppId);
        IQueryable<UserWord> GetUserWords();
        UserWord Add(UserWord userWord);
        UserWord Add(UserWord word, string userKey);
        UserWord Add(UserWord word, string example, int translateLanguage_Id, string translates, string userKey);
        void Delete(int id);
        void Edit(UserWord userWord);
        string GetWordTranslates(string word, string appUserId);
        void ChangeWordsStatus(int status_Id, int[] words);

    }
    public class UserWordsService : IUserWordsService
    {
        public const string DefaultWordSet = "Default";
        IWorkContext _workContext;
        public IPolyglotRepository Repository { get; set; }
        public UserWordsService(IPolyglotRepository repository, IWorkContext workContext)
        {
            Repository = repository;
            _workContext = workContext;
        }

        #region Query
        public IEnumerable<WordSet> GetUserWordsSets(string userAppId)
        {
            IQueryable<WordSet> query = Repository.FindBy<WordSet>(ws => ws.User.ApplicationUserId == userAppId);

            if(query.Count() == 0)
                return new[] { new WordSet() { Id = -1, Name = DefaultWordSet } };
            return query;
        }

        public IQueryable<UserWord> GetUserWords() => Repository.GetAll<UserWord>();
        public IQueryable<UserWord> GetUserWords(string userAppId)
        {
            return Repository.FindBy<UserWord>(uw => uw.Set.User.ApplicationUserId == userAppId);
        }



        public IQueryable<WordType> GetWordTypes()
        {
            return Repository.GetAll<WordType>();
        }

        #endregion

        #region CUD



        public UserWord Add(UserWord userWord) => Add(userWord, _workContext.UserAppId);
        public UserWord Add(UserWord userWord, string userAppId)
        {
            var UserAppId = _workContext.UserAppId;
            UserProfile user = Repository.GetUserProfile(userAppId);

            if(user == null)
                throw new Exception("Пользователь не найден");

            UserWord newUserWord = new UserWord(userWord.Word.Text, userWord.Word.Language_Id, userWord.Status_Id, userWord.Set_Id, user.Id);

            SetWordSet(newUserWord, userWord, user);

            //newUserWord.Word.UserProfile_Id = user.Id;
            newUserWord.Word = Repository.Words.FindSameWordOrDefault(newUserWord.Word);

            if(IsUserHaveWord(Repository.UserWords, newUserWord, UserAppId))
                throw new Exception("Уже есть это слово в этом наборе");
            //UserWord userWord = new UserWord() { Set = wordSet, Status_Id = status.Id, Word = FindWordOrDefault(word) };
            Repository.Add(newUserWord, true);
            return newUserWord;

        }
        public UserWord Add(UserWord userWord, string example, int translateLanguage_Id, string translates, string userAppId)
        {
            UserProfile user = Repository.GetUserProfile(userAppId);
            var newUserWord = Add(userWord, userAppId);

            if(!string.IsNullOrEmpty(example))
            {
                WordDefinition wordDefinition = new WordDefinition() { Word_Id = newUserWord.Word_Id, Example = example, Public = true, UserProfile_Id = user.Id };
                Repository.Add(wordDefinition, true);
            }
            if(!string.IsNullOrWhiteSpace(translates))
            {
                var translatesArr = translates.Split(',').Select(_=>_.Trim().ToLower()).Select(_=>new Word() { Language_Id = translateLanguage_Id, Text = _ });
                var newWord = Repository.GetByKey<Word>(newUserWord.Word_Id);

                foreach(var newWordTranslate in translatesArr)
                {
                    var wordRes = Repository.Words.FindSameWordOrDefault(newWordTranslate, out bool isfound);
                    if(!isfound)
                    {
                        wordRes.UserProfile_Id = user.Id;
                        wordRes.Text = newWordTranslate.Text.ToLower();
                        wordRes.Language_Id = newWordTranslate.Language_Id;
                        wordRes.Type_Id = newWordTranslate.Type_Id;
                        wordRes.DateTimeCreate = DateTime.Now;
                    }

                    newWord.TranslationsTo.Add(wordRes);

                    Repository.Update(newWord);
                }
                Repository.Save();
            }

            return newUserWord;
        }

        public void Add(IEnumerable<string> words, string setName, int lang_id, int status_id)//todo textid unsaved
        {
            var UserAppId = _workContext.UserAppId;
            UserProfile user = Repository.GetUserProfile(UserAppId);

            if(user == null)
                throw new Exception("Пользователь не найден");


            WordSet set = Repository.FindBy<WordSet>(s => s.Name == setName && s.User_Id == user.Id).FirstOrDefault();
            if(set == null)
            {
                set = new WordSet { Name = setName, User = user };
                this.Repository.Add(set, true);
            }
            var wordsExists = Repository.Words.ToList().AsQueryable();
            var userWordsExists = Repository.UserWords.ToList().AsQueryable();
            foreach(string word in words)
            {
                UserWord newUserWord = new UserWord(word, lang_id, status_id, set.Id, user.Id);
                newUserWord.Word = wordsExists.FindSameWordOrDefault(newUserWord.Word);
                if(!IsUserHaveWord(userWordsExists, newUserWord, UserAppId))
                    Repository.Add(newUserWord);
            }
            Repository.Save();
        }

        public void Delete(int id)
        {
            if(!IsUserWordOwning(id, _workContext.UserAppId))
                throw new Exception("Not your word.");
            Repository.Delete<UserWord>(id, true);
        }
        public void Edit(UserWord userWord)
        {
            UserProfile user = Repository.GetUserProfile(_workContext.UserAppId);

            if(!IsUserWordOwning(userWord.Id, _workContext.UserAppId))
            {
                throw new Exception("Not your word!");
            }
            UserWord origin = Repository.GetByKey<UserWord>(userWord.Id, uw => uw.Word);
            //origin.Set_Id = userWord.Set_Id;
            SetWordSet(origin, userWord, user);
            origin.Status_Id = userWord.Status_Id;

            if(userWord.Word.Text.Trim() != origin.Word.Text.Trim() && //если изменился текст
                origin.Word.UserProfile_Id == user.Id && // и пользователь владелец //↓ и нет других пользователей у этого слова.
                Repository.UserProfiles.Where(up => up.UserWordsSets.SelectMany(set => set.UserWords).Where(uw => uw.Word_Id == userWord.Word_Id).Count() > 0).Count() <= 1)
            {
                origin.Word.Text = userWord.Word.Text;
            }

            Repository.Update(origin, true);
        }
        void SetWordSet(UserWord original, UserWord viewModel, UserProfile user)
        {
            if(viewModel.Set_Id > 0) //задан id - ищем по нему
            {
                if((original.Set = Repository.FindBy<WordSet>(s => s.Id == original.Set_Id && s.User_Id == user.Id).FirstOrDefault()) == null) // проверка что пользователю принадлежит указанный набор
                    throw new Exception("Не найден набор у пользователя.");
            }
            else //не задан id - ищем по имени, если не найден создаем.
            {
                string name = !string.IsNullOrWhiteSpace(viewModel.Set?.Name) ? viewModel.Set.Name : DefaultWordSet;

                if((original.Set = Repository.FindBy<WordSet>(s => s.Name == name && s.User_Id == user.Id).FirstOrDefault()) == null)
                {
                    var newWordSet = new WordSet() { Name = name };
                    newWordSet.User = user;
                    original.Set = newWordSet;
                }
            }
        }
        #endregion

        bool IsUserHaveWord(IQueryable<UserWord> query, UserWord userWord, string appUserId)
        {
            return
                userWord.Word_Id > 0 && // если слово новое - не может иметь.
                query.Any(uw =>
                    uw.Set.User.ApplicationUserId == appUserId && //для указанного пользователя
                    uw.Word_Id == userWord.Word_Id && // указанное слово
                    uw.Set_Id == userWord.Set_Id // в указаном наборе
                );
        }

        bool IsUserWordOwning(int userWordId, string appUserId)//todo for words array 
        {
            return Repository.UserWords.Any(uw => uw.Id == userWordId && uw.Set.User.ApplicationUserId == appUserId);
        }

        public string GetWordTranslates(string word, string appUserId)// todo only user translates param?
        {
            string result = null;
            var wordExist =
                GetUserWords(appUserId)
                .Where(w => w.Word.Text == word)
                .IncludeQ(w => w.Word.Language)
                .IncludeQ("Word.TranslationsFrom.Language")
                .IncludeQ("Word.TranslationsTo.Language")
                .FirstOrDefault();

            if(wordExist != null)
            {

                result = $"{wordExist.Word.Text} ({wordExist.Word.Language.ShortName})" + Environment.NewLine;
                var translations = new List<Word>(wordExist.Word.TranslationsTo);
                translations.AddRange(wordExist.Word.TranslationsFrom);

                result += string.Join(
                    Environment.NewLine,
                    translations.GroupBy(w => w.Language.ShortName).Select(w => $"{w.Key}: ({string.Join(", ", w.Select(ww => ww.Text))})")
                    );
            }

            return result;
        }
        public void ChangeWordsStatus(int status_Id, int[] words)
        {
            string userAppId = _workContext.UserAppId;

            foreach(int wordId in words)
            {
                if(!IsUserWordOwning(wordId, userAppId))
                    throw new Exception($"not your word: {wordId} ");

                var word = Repository.GetByKey<UserWord>(wordId);
                word.Status_Id = status_Id;
                Repository.Update(word);
            }

            Repository.Save();
        }
    }
}
