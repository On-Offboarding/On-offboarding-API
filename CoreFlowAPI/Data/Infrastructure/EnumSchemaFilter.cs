using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoreFlowAPI.Data.Infrastructure
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum)
                return;

            var enumValues = Enum.GetValues(context.Type);

            var descriptionLines = enumValues
            .Cast<object>()
            .Select(value =>
            {
                var intValue = (int)value;
                var name = Enum.GetName(context.Type, value);
                return $"{intValue} = {name}";
            });

            schema.Description += "\n\nAllowed values:\n" +
                                  string.Join("\n", descriptionLines);
        }
    }
}
