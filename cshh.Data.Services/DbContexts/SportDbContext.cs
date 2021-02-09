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
    public class SportDbContext : DBContextWithLog
    {

        static TraceSwitch _DbContextSwitch = new TraceSwitch("SportDbContextSwitch", "DbContext Sport TraceSwitch");

        public override TraceSwitch TraceSwitch => _DbContextSwitch;

        static SportDbContext()
        {
            Database.SetInitializer<SportDbContext>(null);
        }

        public SportDbContext(string connectionString) : base(connectionString)
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
        public SportDbContext() : base()
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
            modelBuilder.Configurations.Add(new Mapping.Sport.SetMap());            
        }

        public DbSet<cshh.Data.Sport.Set> Sets { get; set; }        

    }

}
