using cshh.Data.Services.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public class PolyglotEfRepository : EfRepository, IPolyglotRepository
    {
        public PolyglotEfRepository(PolyglotDbContext context ) : base(context)
        {

        }
    }
}
