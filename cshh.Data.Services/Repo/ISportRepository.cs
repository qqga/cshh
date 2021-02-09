using System;
using System.Linq;

namespace cshh.Data.Services.Repo
{
    public interface ISportRepository : IRepository
    {
        IQueryable<Data.Sport.Set> Sets { get; }
    }
}
