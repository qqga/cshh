using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using cshh.Data.Polyglot;
using cshh.Data.User;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Core.Objects;

namespace cshh.Data.Services.DbContexts
{
    public class PolyglotDbContext : DbContext
    {

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        static PolyglotDbContext()
        {
            Database.SetInitializer<PolyglotDbContext>(null);
        }

        public const string DefaultConnectionString = "cshhConnection";
        public PolyglotDbContext(string connectionString) : base(connectionString)
        {
            this.Database.Log = LogContext;
        }
        /// <summary>
        /// Constructs a new context instance using conventions to create the name of the database to
        /// which a connection will be made.  The by-convention name is the full name (namespace + class name)
        /// of the derived context class.
        /// See the class remarks for how this is used to create a connection.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public PolyglotDbContext() : this(DefaultConnectionString)
        {
            this.Database.Log = LogContext;
        }

        static TraceSwitch _PolyglotDbContextSwitch = new TraceSwitch("PolyglotDbContextSwitch", "DbContext Polyglot TraceSwitch");

        //можно поменять на IDbCommandInterceptor, будет работать на все контексты (см. configuration.cs)
        void LogContext(string s)
        {
            switch(_PolyglotDbContextSwitch.Level)
            {
                case TraceLevel.Off:
                    break;

                case TraceLevel.Error:
                case TraceLevel.Warning:
                case TraceLevel.Info:
                    if(!string.IsNullOrWhiteSpace(s))
                        Trace.TraceInformation($"PolyglotDbContext:{Environment.NewLine}{s}{Environment.NewLine}");
                    break;

                case TraceLevel.Verbose:
                    Trace.WriteLine($"----------PolyglotDbContext--------------{Environment.NewLine}{s}{Environment.NewLine}================");
                    break;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new Mapping.UserProfileMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.ForeignTextMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.BookmarkMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.LanguageMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.UserWordMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.WordDefinitionMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.WordMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.WordSetMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.WordStatusMap());
            modelBuilder.Configurations.Add(new Mapping.Polyglot.WordTypeMap());
        }
        public override int SaveChanges()
        {
            //var entries = this.ChangeTracker.Entries();
            return base.SaveChanges();
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<ForeignText> ForeignTexts { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<WordDefinition> WordDefinitions { get; set; }
        public DbSet<WordSet> WordSets { get; set; }
        public DbSet<WordStatus> WordStatuses { get; set; }
        public DbSet<WordType> WordTypes { get; set; }

    }
}
