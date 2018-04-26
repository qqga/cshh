using cshh.Data.Polyglot;
using cshh.Data.Services.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services.Polyglot
{
    public interface IPolyglotListsService
    {
        IQueryable<Language> GetLanguaches();
        IQueryable<WordType> GetWordTypes();
        IQueryable<WordStatus> GetWordStatuses();
    }
    
    public class PolyglotListsService : IPolyglotListsService
    {
        IPolyglotRepository _PolyglotRepository;
        public PolyglotListsService(IPolyglotRepository polyglotRepository)
        {
            _PolyglotRepository = polyglotRepository;
        }

        public IQueryable<Language> GetLanguaches() => _PolyglotRepository.GetAll<Language>();
        public IQueryable<WordType> GetWordTypes() => _PolyglotRepository.GetAll<WordType>();
        public IQueryable<WordStatus> GetWordStatuses() => _PolyglotRepository.GetAll<WordStatus>();
    }

}
