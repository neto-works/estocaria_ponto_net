using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EstocariaNet.Shared.Swagger
{
    public class FilterSwagger : IDocumentFilter
    {
        private readonly IEnumerable<Type> _typesToExclude;

        public FilterSwagger(IEnumerable<Type> typesToExclude)
        {
            _typesToExclude = typesToExclude;
        }

        //methods
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var excludedType in _typesToExclude)
            {
                var schemaToRemove = swaggerDoc.Components.Schemas.FirstOrDefault(s => s.Key == excludedType.Name);
                if (!string.IsNullOrEmpty(schemaToRemove.Key))
                {
                    swaggerDoc.Components.Schemas.Remove(schemaToRemove.Key);
                }
            }
        }
    }
}