using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yoli.WebApi.Swagger;

public class AddAuthHeaderOperationFilter : IOperationFilter
{
    public AddAuthHeaderOperationFilter()
    {
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var filterDescriptor = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        var filters = filterDescriptor.Select(filterInfo => filterInfo.Filter);

        //var isAuthorized = filters.Any(filter => filter is AuthorizeFilter);
        var isAuthorized = filters.Any(filter => filter is IAuthorizationFilter);
        var allowAnonymous = filters.Any(filter => filter is IAllowAnonymousFilter);

        if (!isAuthorized || allowAnonymous)
        {
            return;
        }

        operation.Security = new List<OpenApiSecurityRequirement>();

        var req = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        // Definition name. 
                        // Should exactly match the one given in the service configuration
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        };

        //Add JWT bearer type
        operation.Security.Add(req);
        
    }
}
