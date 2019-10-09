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
    public class PolyglotDbContext : DBContextWithLog
    {
        static TraceSwitch _PolyglotDbContextSwitch = new TraceSwitch("PolyglotDbContextSwitch", "DbContext Polyglot TraceSwitch");
        public override TraceSwitch TraceSwitch => _PolyglotDbContextSwitch;

        static PolyglotDbContext()
        {
            Database.SetInitializer<PolyglotDbContext>(null);
        }

        public PolyglotDbContext(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// Constructs a new context instance using conventions to create the name of the database to
        /// which a connection will be made.  The by-convention name is the full name (namespace + class name)
        /// of the derived context class.
        /// See the class remarks for how this is used to create a connection.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public PolyglotDbContext() : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            MapConfiguration(modelBuilder);
        }

        internal static void MapConfiguration(DbModelBuilder modelBuilder)
        {
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
