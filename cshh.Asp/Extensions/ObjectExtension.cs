using cshh.Asp.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;

namespace cshh.Asp
{
    public static class ObjectExtension
    {
        public static string ToJson<T>(this object obj) where T : class
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new JsonTypeContractResolver<T>() };
            var serializedObject = JsonConvert.SerializeObject(obj, settings);

            return serializedObject;
        }
    }
}