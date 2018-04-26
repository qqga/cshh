using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class UserWord : BaseEntity
    {
        public UserWord()
        {
        }
        public UserWord(string text, int lang_Id, int status_Id, int set_Id, int userProfile_Id)
        {
            Status_Id = status_Id;
            Set_Id = set_Id;
            
            Word = new Word { Language_Id = lang_Id, Text = text, UserProfile_Id = userProfile_Id };
            Word.DateTimeCreate = DateTimeCreate = DateTime.Now;
        }
        public Word Word { get; set; }
        public int Word_Id { get; set; }

        public WordStatus Status { get; set; }
        public int Status_Id { get; set; }

        public WordSet Set { get; set; }
        public int Set_Id { get; set; }

        public DateTime DateTimeCreate { get; set; }

    }
}
