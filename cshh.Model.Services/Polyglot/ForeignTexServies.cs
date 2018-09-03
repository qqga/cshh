using cshh.Data.Polyglot;
using cshh.Data.Services.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cshh.Model.Services.Polyglot
{
    public interface IForeignTextService
    {
        void Add(ForeignText foreignText);
        void Edit(ForeignText foreignText);
        void Delete(int foreignText);
        IQueryable<ForeignText> GetUserTexts();
        string[] GetDefaultSeparators();
        IQueryable<ForeignText> GetTexts();
        IEnumerable<WordInfo> ParseWords(string text, string[] separators);
        Bookmark AddBookMark(Bookmark bookmark);
        void DeleteBookmark(int id);
        Bookmark EditBookmark(Bookmark bookmark);
    }

    public class ForeignTextService : IForeignTextService
    {
        IPolyglotRepository PolyglotRepository { get; set; }
        IWorkContext _WorkContext;
        public ForeignTextService(IPolyglotRepository polyglotRepository, IWorkContext workContext)
        {
            PolyglotRepository = polyglotRepository;
            _WorkContext = workContext;
        }

        public IQueryable<ForeignText> GetUserTexts()
        {
            return this.PolyglotRepository.GetAll<ForeignText>();
        }

        #region CUD
        public void Add(ForeignText foreignText)
        {

            ForeignText newText = new ForeignText()
            {
                Language_Id = foreignText.Language_Id,
                Name = foreignText.Name,
                Text = foreignText.Text,
                UserProfile_Id = PolyglotRepository.GetUserProfile(_WorkContext.UserAppId).Id,

            };

            PolyglotRepository.Add(newText, true);
        }

        public void Edit(ForeignText foreignText)
        {
            ForeignText origin = CheckUserOwning(foreignText.Id);

            origin.Language_Id = foreignText.Language_Id;
            origin.Name = foreignText.Name;
            origin.Text = foreignText.Text;

            PolyglotRepository.Update(origin, true);
        }

        public void Delete(int foreignTextId)
        {
            CheckUserOwning(foreignTextId);
            PolyglotRepository.Delete<ForeignText>(foreignTextId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreignTextId"></param>
        /// <returns>returns original </returns>
        ForeignText CheckUserOwning(int foreignTextId)
        {
            ForeignText foreignText = PolyglotRepository.ForeignTexts.FirstOrDefault(t => t.Id == foreignTextId && t.UserProfile.ApplicationUserId == _WorkContext.UserAppId);
            if(foreignText == null)
                throw new Exception("Текст не найден или не принадлежит пользователю.");
            return foreignText;
        }

        #endregion

        public string[] GetDefaultSeparators()
        {
            return WordCounter.DefaultSeparators;
        }
        public IQueryable<ForeignText> GetTexts() => PolyglotRepository.GetAll<ForeignText>();
        public IEnumerable<WordInfo> ParseWords(string text, string[] separators)
        {
            WordCounter wc = new WordCounter(text, separators);
            WordInfo[] wordsInfo = wc.GetWordsInfo();
            return wordsInfo;
        }
        public Bookmark AddBookMark(Bookmark bookmark) //todo add title, note (angular?)
        {
            CheckUserOwning(bookmark.ForeignText_Id);

            Bookmark newBookmark = new Bookmark()
            {
                ForeignText_Id = bookmark.ForeignText_Id,
                Note = bookmark.Note,
                Position = bookmark.Position,
                Title = bookmark.Title
            };

            PolyglotRepository.Add(newBookmark, true);

            return newBookmark;
        }
        public void DeleteBookmark(int id)
        {
            Bookmark bookmark = PolyglotRepository.GetByKey<Bookmark>(id);
            CheckUserOwning(bookmark.ForeignText_Id);
            PolyglotRepository.Delete(bookmark, true);
        }
        public Bookmark EditBookmark(Bookmark bookmark)
        {
            Bookmark bookmarkOrigin = PolyglotRepository.GetByKey<Bookmark>(bookmark.Id);

            CheckUserOwning(bookmarkOrigin.ForeignText_Id);

            bookmarkOrigin.Title = bookmark.Title;
            bookmarkOrigin.Note = bookmark.Note;

            PolyglotRepository.Update(bookmarkOrigin, true);

            return bookmarkOrigin;
        }
    }

}
