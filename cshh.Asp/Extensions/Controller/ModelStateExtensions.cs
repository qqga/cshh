using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Extensions
{
    public static class ModelStateExtensions
    {
        public static string CollectErrors(this ModelStateDictionary dictionary, string separator= "<br />")
        {
            return
                string.Join(separator,
                dictionary
                .Where(kv=> kv.Value.Errors.Count > 0)                
                .Select(kv => kv.Key + ": " + string.Join(", ", kv.Value.Errors.Select(err => err.ErrorMessage))
                ));
        }
    }
}