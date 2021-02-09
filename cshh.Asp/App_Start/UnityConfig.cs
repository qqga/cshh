using AutoMapper;
using cshh.Data.Polyglot;
using cshh.Data.Services.DbContexts;
using cshh.Data.Services.Repo;
using cshh.Model.Services.Polyglot;
using cshh.Model.Services.Sport;
using cshh.Model.Services.Tasks;
using cshh.Model.Services.User;
using cshh.Model.Services.Viber;
using System;
using System.Web.Configuration;
using Unity;
using Unity.Injection;

namespace cshh.Asp
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();


            //System.Web.Mvc.ModelBinders.Binders.DefaultBinder = new Common.Asp.ModelBinder.DIModelBinder();


            var appSettingsEncryptedSection = WebConfigurationManager.GetSection("appSettingsEncrypted") as System.Collections.Specialized.NameValueCollection;

            #region Repository
            container.RegisterType<IUserRepository, PolyglotEfRepository>();
            container.RegisterType<IPolyglotRepository, PolyglotEfRepository>();
            container.RegisterType<ITaskRepository, TaskEfRepository>();
            container.RegisterType<IViberRepository, ViberEfRepository>();
            container.RegisterType<ISportRepository, SportEfRepository>();

            container.RegisterType<PolyglotDbContext>(new Unity.Lifetime.PerResolveLifetimeManager(),
                new InjectionFactory(c => new PolyglotDbContext("cshhConnection")));
            //container.RegisterSingleton<PolyglotDbContext>(new InjectionFactory(c=> new PolyglotDbContext("cshhConnection") ));

            container.RegisterType<TaskDbContext>(new Unity.Lifetime.PerResolveLifetimeManager(),
                new InjectionFactory(c => new TaskDbContext("cshhConnection")));

            container.RegisterType<ViberDbContext>(new Unity.Lifetime.PerResolveLifetimeManager(),
                new InjectionFactory(c => new ViberDbContext("cshhConnection")));

            container.RegisterType<SportDbContext>(new Unity.Lifetime.PerResolveLifetimeManager(),
                new InjectionFactory(c => new SportDbContext("cshhConnection")));

            #endregion

            #region Model Serivces
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IWordsService, WordsService>();
            container.RegisterType<IUserWordsService, UserWordsService>();
            container.RegisterType<IWordDefinitionService, WordDefinitionService>();
            container.RegisterType<IPolyglotListsService, PolyglotListsService>();
            container.RegisterType<IForeignTextService, ForeignTextService>();

            container.RegisterType<ITaskService, TaskService>();

            container.RegisterType<ViberBot.ViberBotClient>(new InjectionFactory(s => new ViberBot.ViberBotClient(appSettingsEncryptedSection["ViberToken"])));                
            container.RegisterType<IViberBot, Models.Viber.ViberBotAdapter>();
            container.RegisterType<IViberService, ViberService>();
                        
            container.RegisterType<IWorkoutService, WorkoutService>();

            #endregion

            #region Mapping
            container.RegisterInstance<IMapper>(new MapperConfiguration(cfg => cfg.AddProfile(new DefaultPolyglotMapProfile())).CreateMapper());

            #endregion

            container.RegisterType<Model.Services.IWorkContext, Models.Common.WorkContext>();


            //container.RegisterType<Common.Asp.JqGrid.JqGridRequest>(new InjectionConstructor(new Func<string, Type, object>(Newtonsoft.Json.JsonConvert.DeserializeObject)));

            //Func<string, Common.Asp.JqGrid.Filter> f = new Func<string, Common.Asp.JqGrid.Filter>(s=> Newtonsoft.Json.JsonConvert.DeserializeObject<Common.Asp.JqGrid.Filter>(s));
            //container.RegisterType<Func<string, Common.Asp.JqGrid.Filter>>(new InjectionFactory(c =>
            //new Func<string, Common.Asp.JqGrid.Filter>(s => Newtonsoft.Json.JsonConvert.DeserializeObject<Common.Asp.JqGrid.Filter>(s))
            //));


        }
    }
}