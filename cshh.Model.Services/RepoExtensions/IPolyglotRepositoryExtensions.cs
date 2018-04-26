using cshh.Data.Polyglot;
using cshh.Data.Services.Repo;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services
{
    public static class IPolyglotRepositoryExtensions
    {
        public static Word FindSameWordOrDefault(this IQueryable<Word> wodsQuery, Word word, out bool isFound)
        {
            var foundWord = FindSameWordOrDefault(wodsQuery, word);
            isFound = foundWord != word;
            return foundWord;
        }

        public static Word FindSameWordOrDefault(this IQueryable<Word> wodsQuery, Word word)
        {
            return wodsQuery.Where(w =>
            w.Text == word.Text &&
            w.Language_Id == word.Language_Id
            ).FirstOrDefault() ?? word;
        }

        //public UserProfile GetUserProfile(this IPolyglotRepository repository, string appUserId)
        //{            
        //}
    }
}
