using cshh.Data.Polyglot;
using cshh.Data.Services.DbContexts;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public class PolyglotEfRepository : EfRepository, IPolyglotRepository
    {
        public PolyglotEfRepository(PolyglotDbContext context) : base(context)
        {

        }

        public IQueryable<Word> Words => Context.Set<Word>();
        public IQueryable<ForeignText> ForeignTexts => Context.Set<ForeignText>();
        public IQueryable<Language> Languages => Context.Set<Language>();
        public IQueryable<UserWord> UserWords => Context.Set<UserWord>();
        public IQueryable<WordDefinition> WordDefinitions => Context.Set<WordDefinition>();
        public IQueryable<WordSet> WordSets => Context.Set<WordSet>();
        public IQueryable<WordStatus> WordStatuses => Context.Set<WordStatus>();
        public IQueryable<WordType> WordTypes => Context.Set<WordType>();
        public IQueryable<UserProfile> UserProfiles => Context.Set<UserProfile>();
    }
}
