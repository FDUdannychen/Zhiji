using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Zhiji.Common.Json
{
    public class DeclaredPropertiesResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            foreach (var property in properties)
            {
                if (!property.DeclaringType.Equals(type)) property.Ignored = true;
            }

            return properties;
        }
    }
}
