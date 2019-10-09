using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Viber
{
    public class SendMessageViewModel
    {
        public SendMessageViewModel(IEnumerable<Data.Viber.ViberUser> usersTo, IEnumerable<Data.Viber.ViberUser> usersFrom)
        {
            UsersTo = usersTo;
            UsersFrom = usersFrom;
        }
        public IEnumerable<Data.Viber.ViberUser> UsersFrom { get; set; } = new List<Data.Viber.ViberUser>();
        public IEnumerable<Data.Viber.ViberUser> UsersTo { get; set; } = new List<Data.Viber.ViberUser>();

    }
}