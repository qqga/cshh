using cshh.Data.Polyglot;
using cshh.Data.User;
using System;
using System.Data.Entity;
using System.Linq;

namespace cshh.Data.Services.Repo
{
    public interface IPolyglotRepository : IUserRepository
    {
        //IQueryable<UserProfile> UserProfiles { get; set; }
        IQueryable<Word> Words { get; }
        IQueryable<ForeignText> ForeignTexts { get; }
        IQueryable<Language> Languages { get; }
        IQueryable<UserWord> UserWords { get; }
        IQueryable<WordDefinition> WordDefinitions { get; }
        IQueryable<WordSet> WordSets { get; }
        IQueryable<WordStatus> WordStatuses { get; }
        IQueryable<WordType> WordTypes { get; }

    }
}
