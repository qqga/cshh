using cshh.Data.Services.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public interface ILogRepository : IRepository
    {

    }

    public class LogEfRepository : EfRepository, ILogRepository
    {
        public LogEfRepository(LogContext context) : base(context)
        {

        }
    }
}
