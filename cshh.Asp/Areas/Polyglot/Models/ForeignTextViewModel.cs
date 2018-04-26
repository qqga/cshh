using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Polyglot
{
    #region RegionName
    public class ForeignTextViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        #endregion
        public string Text { get; set; }
        public string TextShort { get; set; }
        public string LanguageShortName { get; set; } 
        public string Language_Id { get; set; }
    }


    public class ForeignTextListstViewModel
    {
        public string Languages { get; set; }
    }
}