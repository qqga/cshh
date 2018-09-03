using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.DbContexts
{
    public abstract class DBContextWithLog : DbContext
    {        
        public const string DefaultConnectionString = "cshhConnection";
        public abstract TraceSwitch TraceSwitch { get; }

        //можно поменять на IDbCommandInterceptor, будет работать на все контексты (см. configuration.cs)
        void LogContext(string s)
        {
            string className = this.GetType().Name;
            switch(TraceSwitch.Level)
            {
                case TraceLevel.Off:
                    break;

                case TraceLevel.Error:
                case TraceLevel.Warning:
                case TraceLevel.Info:
                    if(!string.IsNullOrWhiteSpace(s))
                        Trace.TraceInformation($"{className}:{Environment.NewLine}{s}{Environment.NewLine}");
                    break;

                case TraceLevel.Verbose:
                    Trace.WriteLine($"----------{className}--------------{Environment.NewLine}{s}{Environment.NewLine}================");
                    break;
            }
        }

        public DBContextWithLog(string connectionString) : base(connectionString)
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
        public DBContextWithLog() : this(DefaultConnectionString)
        {
            this.Database.Log = LogContext;
        }

    }
}
