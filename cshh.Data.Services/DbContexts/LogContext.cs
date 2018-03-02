using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.DbContexts
{
    public class LogContext : DbContext
    {
        static LogContext()
        {
            Database.SetInitializer<LogContext>(null);
        }

        public const string DefaultConnectionString = "cshhConnection";
        public LogContext(string connectionString) : base(connectionString)
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
        public LogContext() : this(DefaultConnectionString)
        {


        }

        //public DbSet<LogAction> LogAction { get; set; }
    }
}
