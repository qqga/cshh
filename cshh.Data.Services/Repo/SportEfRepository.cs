using cshh.Data.Polyglot;
using cshh.Data.Services.DbContexts;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public class SportEfRepository : EfRepository, ISportRepository
    {
        public SportEfRepository(SportDbContext context) : base(context)
        {

        }

        
        public IQueryable<Data.Sport.Set> Sets => Context.Set<Data.Sport.Set>();        
        
    }
}
