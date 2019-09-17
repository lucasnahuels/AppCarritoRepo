using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace ApiServices.Swagger
{
    public sealed class ConfigureSwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly SwaggerSettings settings;
        public ConfigureSwaggerUiOptions(IApiVersionDescriptionProvider versionDescriptionProvider, IOptions<SwaggerSettings> settings)
        {
            this.provider = versionDescriptionProvider;
            this.settings = settings?.Value ?? new SwaggerSettings();
        }
        public void Configure(SwaggerUIOptions options)
        {
            provider
           .ApiVersionDescriptions
           .ToList()
           .ForEach(description => {
               options.SwaggerEndpoint($"/{settings.RoutePrefixWithSlash}{description.GroupName}/swagger.json",
               description.GroupName.ToUpperInvariant());
               options.RoutePrefix = settings.RoutePrefix;
           });
        }
    }
}
