using cshh.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace cshh.Asp.Models.Common
{
    //todo move out
    public class ValidationDictionary : IValidationDictionary
    {
        public ModelStateDictionary ModelStateDictionary { get; }

        public ValidationDictionary(ModelStateDictionary modelStateDictionary)
        {
            ModelStateDictionary = modelStateDictionary;
        }

        public bool IsValid => ModelStateDictionary.IsValid;

        public void AddError(string key, string errorMessage)
        {
            ModelStateDictionary.AddModelError(key, errorMessage);
        }
    }
}