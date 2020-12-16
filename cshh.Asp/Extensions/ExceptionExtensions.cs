using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Extensions
{
    public static class ExceptionExtensions
    {
        public static string CollectMessages(this Exception e, string separator = "<br />")
        {
            List<string> messages = new List<string>();
            Action<Exception> func = null;
            func = (ex) =>
             {
                 messages.Add(ex.Message);

                 if(ex is System.Data.Entity.Validation.DbEntityValidationException eValidEx)
                 {
                     foreach(System.Data.Entity.Validation.DbEntityValidationResult dbEntityValidationResult in eValidEx.EntityValidationErrors)
                     {
                         if(!dbEntityValidationResult.IsValid)
                         {
                             string entityName = dbEntityValidationResult.Entry.Entity.GetType().Name;
                             IEnumerable<string> errors = dbEntityValidationResult.ValidationErrors.Select(ve => $"{ve.PropertyName}: { ve.ErrorMessage}");
                             messages.Add($"{entityName}: '{string.Join(",",errors)}'");
                         }
                     }
                 }

                 if(ex.InnerException != null)
                     func(ex.InnerException);
             };



            func(e);

            return string.Join(separator, messages);
        }
    }
}