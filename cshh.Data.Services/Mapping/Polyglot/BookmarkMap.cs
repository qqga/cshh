using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class BookmarkMap : BaseEntityTypeConfiguration<Bookmark>
    {

        public BookmarkMap() : base()
        {
            ToTable("Bookmark", "Polyglot");

            Property(t => t.Title).IsRequired().HasMaxLength(200).IsUnicode();
            Property(t => t.Note).IsRequired().IsMaxLength().IsUnicode();
            
            HasRequired(t => t.ForeignText).WithMany(t => t.Bookmarks).HasForeignKey(b => b.ForeignText_Id).WillCascadeOnDelete(true);
            
        }
    }
}
