using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Extensions
{


    public class JsonTypeContractResolver<T> : DefaultContractResolver where T : class
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(typeof(T), memberSerialization);
            return properties;
        }
    }

    public class JsonTypeContractResolver : DefaultContractResolver
    {
        public JsonTypeContractResolver(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(Type, memberSerialization);
            return properties;
        }
    }
}