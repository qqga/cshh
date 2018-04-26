using cshh.Data.Services.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services
{
    public interface IRepoContainer: IDisposable
    {
        IRepository Repository { get; set; }
    }
}
