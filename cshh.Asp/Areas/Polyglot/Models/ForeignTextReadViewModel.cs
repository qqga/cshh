using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Polyglot
{
   
    public class ForeignTextReadViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Text { get; set; }
        public IEnumerable<BookmarkViewModel> Bookmarks { get; set; }

        public string GetTextWithBookmarks(Func<string,string> httmlEncode, Func<string, string, string> bookmarkFunk)
        {
            string text = httmlEncode(Text);
            foreach(BookmarkViewModel bookmark in Bookmarks)
            {
                string bookmarkTextResult = bookmarkFunk(bookmark.Title, bookmark.Note);
                text = text.Insert(bookmark.Position, bookmarkTextResult);
            }
            return text;
        }
    }

    public class BookmarkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int Position { get; set; }

    }
}