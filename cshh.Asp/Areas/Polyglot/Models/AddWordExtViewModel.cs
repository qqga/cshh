using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Models.Polyglot
{
    public class AddWordExtViewModel
    {

        public string SetName { get; set; }
        public string WordText { get; set; }
        public int WordLanguage_Id { get; set; }
        public int Status_Id { get; set; }

        public string ExistWordInfo { get; set; }
        public string UserKey { get; set; }
        public string UrlPost { get; set; }

        public IEnumerable<string> Sets { get; set; } = new string[] { };
        public IEnumerable<SelectListItem> Languages { get; set; } = new SelectListItem[] { };
        public IEnumerable<SelectListItem> Statuses { get; set; } = new SelectListItem[] { };

    }

}