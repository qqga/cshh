using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services
{
    public interface IWorkContext
    {
        string UserAppId { get;}

        //ClaimsIdentity ClaimsIdentity { get; }
    }
}
