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
    public interface IWordDefinitionService //: IRepoContainer
    {
        IPolyglotRepository Repository { get; set; }

        void Delete(int id);
        bool IsUserWordDefenition(int wordDefenitionId, string userAppId);
        IQueryable<WordDefinition> GetWordDefinitions(int word_Id);
        WordDefinition GetWordDefinition(int id);
        void Edit(ref WordDefinition model);
        void Add(WordDefinition model);
    }
    public class WordDefinitionService : IWordDefinitionService
    {
        IWorkContext _workContext;
        public WordDefinitionService(IPolyglotRepository repository, IWorkContext workContext)
        {
            Repository = repository;
            _workContext = workContext;
        }
        public IPolyglotRepository Repository { get; set; }


        public bool IsUserWordDefenition(int wordDefenitionId, string currUserAppId)
        {
            WordDefinition wordDef = Repository.GetByKey<WordDefinition>(wordDefenitionId, w => w.UserProfile);

            return wordDef.UserProfile.ApplicationUserId == currUserAppId;
        }


        public IQueryable<WordDefinition> GetWordDefinitions(int word_Id)
        {
            var wordDefinitions =
                Repository.FindBy<WordDefinition>(d => d.Word.Id == word_Id)
                .IncludeQ(d => d.UserProfile)
                .IncludeQ(d => d.Type);

            return wordDefinitions;
        }

        public WordDefinition GetWordDefinition(int id)
        {
            return Repository.GetByKey<WordDefinition>(id);
        }

        public void Add(WordDefinition model)
        {
            UserProfile userProfile = this.Repository.GetUserProfile(_workContext.UserAppId);

            WordDefinition newWordDefinition = new WordDefinition()
            {
                Definition = model.Definition,
                Public = model.Public,
                Example = model.Example,
                UserProfile_Id = userProfile.Id,
                Type_Id = model.Type_Id,
                Word_Id = model.Word_Id,
            };

            Repository.Add(newWordDefinition, true);
        }
        public void Edit(ref WordDefinition model)
        {
            WordDefinition origin = CheckUserOwning(model.Id);

            origin.Definition = model.Definition;
            origin.Example = model.Example;
            origin.Public = model.Public;
            origin.Type_Id = model.Type_Id;

            Repository.Update(origin, true);
        }

        public void Delete(int id)
        {
            CheckUserOwning(id);
            Repository.Delete<WordDefinition>(id, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns original record from db</returns>
        WordDefinition CheckUserOwning(int wordDefinition_Id)
        {
            WordDefinition origin = this.Repository.GetByKey<WordDefinition>(wordDefinition_Id, d => d.UserProfile);
            if(origin.UserProfile.ApplicationUserId != _workContext.UserAppId)
                throw new Exception("Оперделение не пренадлежит пользователю.");
            return origin;
        }
    }
}
