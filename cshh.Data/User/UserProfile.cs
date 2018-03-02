using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.User
{
    //[Table("UserProfiles", Schema = "User")]
    public class UserProfile : BaseEntity
    {
        //[Required]
        public string ApplicationUserId { get; set; }

        //public List<UserStores> UserStores { get; set; }
        //public List<Store> Stores { get; set; }
        //public List<Order> Orders { get; set; }
        //public List<OrderAddress> OrderAddress { get; set; } = new List<Data.OrderAddress>();

        //public bool AskStoreHolderRole { get; set; }

        public virtual ICollection<ForeignText> ForeignTexts { get; set; }
     // public virtual ICollection<WordSet> WordSets { get; set; }
        public virtual ICollection<UserWord> UserWords { get; set; }
        public virtual ICollection<WordDefinition> WordDefinitions { get; set; }


    }
}
