using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zhiji.Common.Api
{
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
            => swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(p => MakeLowerCase(p.Key), p => p.Value);

        private string MakeLowerCase(string key)
        {
            var shouldLowerCase = true;
            return new string(key.Select(c =>
            {
                if (c == '{') shouldLowerCase = false;
                if (c == '}' || c == '/') shouldLowerCase = true;
                if (char.IsUpper(c) && shouldLowerCase) return char.ToLowerInvariant(c);
                return c;
            }).ToArray());
        }
    }
}
