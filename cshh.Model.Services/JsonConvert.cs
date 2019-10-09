using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services
{
    internal static class JsonConvertExtension
    {
        public static string ToJsonString<T>(this T obj)
        {            
            string serializeObject = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return serializeObject;
        }
    }
}
