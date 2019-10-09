using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Viber
{
    public class ViberUser : BaseEntity
    {
        public string ViberId { get; set; }        
        public string Name { get; set; }
        public string ApiToken { get; set; }

        public int? UserProfile_Id { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
