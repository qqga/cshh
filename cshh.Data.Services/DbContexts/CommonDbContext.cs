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
    public class CommonDbContext : DBContextWithLog
    {
        static TraceSwitch _CommonDbContextSwitch = new TraceSwitch("CommonDbContextSwitch", "DbContext Common TraceSwitch");
        public override TraceSwitch TraceSwitch => _CommonDbContextSwitch;

        static CommonDbContext()
        {
            Database.SetInitializer<CommonDbContext>(null);
        }

        public CommonDbContext(string connectionString) : base(connectionString)
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
        public CommonDbContext() : base()
        {

        }
               
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            PolyglotDbContext.MapConfiguration(modelBuilder);
            TaskDbContext.MapConfiguration(modelBuilder);
            
        }
        public override int SaveChanges()
        {
            //var entries = this.ChangeTracker.Entries();
            return base.SaveChanges();
        }

        public DbSet<UserProfile> UserProfiles { get; set; }


        #region Polyglot
        public DbSet<Word> Words { get; set; }
        public DbSet<ForeignText> ForeignTexts { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<WordDefinition> WordDefinitions { get; set; }
        public DbSet<WordSet> WordSets { get; set; }
        public DbSet<WordStatus> WordStatuses { get; set; }
        public DbSet<WordType> WordTypes { get; set; }
        #endregion


        #region Tasks
        public DbSet<cshh.Data.Tasks.Task> Tasks { get; set; }

        #endregion

    }
}
