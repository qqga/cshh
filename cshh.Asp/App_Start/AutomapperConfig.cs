using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Mappers;
using cshh.Asp.Models.Polyglot;
using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp
{

    //public interface IAutoMappConfigurator
    //{
    //    void Configure(IMapperConfigurationExpression mapperConfig);
    //}
    //public class DefaultPolyglotAutoMappConfig : IAutoMappConfigurator
    //{
    //    public void Configure(IMapperConfigurationExpression mapperConfig)
    //    {
    //       
    //    }
    //}

    public class DefaultPolyglotMapProfile : Profile
    {
        public DefaultPolyglotMapProfile()
        {
            //todo   
            AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "ViewModel");
            AddConditionalObjectMapper().Where((s, d) => s.Name + "ViewModel" == d.Name);

            ForAllMaps((tm, exp) => exp.ValidateMemberList(MemberList.None));

            //CreateMap<Word, WordViewModel>().ReverseMap().ValidateMemberList(MemberList.None);

            UserWordMapper.Map(this);

            CreateMap<ForeignText, ForeignTextViewModel>()
                .ForMember(t => t.TextShort, m => m.MapFrom((src) => string.Join("", src.Text.Take(1000)) + "..."));
        }

        class UserWordMapper
        {
            public static void Map(Profile profile)
            {
                profile.CreateMap<UserWord, UserWordJqViewModel>()
                .ForMember(m => m.Translations, m => m.MapFrom(userWord => TranslationsToString(userWord.Word)))
                .ReverseMap()
                .ForMember(m => m.Word, m => m.MapFrom(userWordVM => UserWordWMToWord(userWordVM)))
                .ValidateMemberList(MemberList.None);
            }
            static Word UserWordWMToWord(UserWordJqViewModel userWordJqVM)
            {
                return new Word()
                {
                    Id = userWordJqVM.Word_Id,
                    Text = userWordJqVM.WordText?.Trim().ToLower(),
                    Language_Id = userWordJqVM.WordLanguage_Id,
                    //Language = new Data.Polyglot.Language() { Id = this.Language_Id },
                    Type_Id = userWordJqVM.WordType_Id,
                    //WordType = new WordType() { Id = this.Type_Id.Value },
                };
            }
            static string TranslationsToString(Word word)
            {
                return string.Join(/*"; "*/Environment.NewLine,
                         word.TranslationsFrom
                         .Union(word.TranslationsTo)
                         .GroupBy(t => t.Language.ShortName)
                         .Select(gr => $"{gr.Key}: {string.Join(", ", gr.Select(w => w.Text)) }"));
            }
        }

    }


    public static class MapperExtensions
    {

        static void MethodName()
        {

        }
    }
}