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
    public class ViberDbContext : DBContextWithLog
    {

        static TraceSwitch _PolyglotDbContextSwitch = new TraceSwitch("ViberDbContextSwitch", "DbContext Viber TraceSwitch");

        public override TraceSwitch TraceSwitch => _PolyglotDbContextSwitch;

        static ViberDbContext()
        {
            Database.SetInitializer<TaskDbContext>(null);
        }

        public ViberDbContext(string connectionString) : base(connectionString)
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
        public ViberDbContext() : base()
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
            modelBuilder.Configurations.Add(new Mapping.Viber.ViberEventMap());
            modelBuilder.Configurations.Add(new Mapping.Viber.ViberUserMap());
        }

        //public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<cshh.Data.Viber.ViberEvent> ViberEvents { get; set; }
        public DbSet<cshh.Data.Viber.ViberUser> ViberUsers { get; set; }

    }

}
