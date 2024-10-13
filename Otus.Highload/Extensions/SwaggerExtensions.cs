using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Otus.Highload.Extensions;

/// <summary>
/// Extensions for swagger configuration.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Register swagger services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "social-network",
                Description = "Social network",
                Contact = new OpenApiContact
                {
                    Name = "Sasha Andriyanich",
                    Email = "SA",
                }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });
            
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        return services;
    }

    /// <summary>
    /// Add swagger to request pipeline.
    /// </summary>
    /// <param name="builder">Application builder.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder)
    {
        builder.UseSwagger(u =>
        {
            u.RouteTemplate = "swagger/{documentName}/swagger.json";
        });

        builder.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "swagger";
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Social network api V1");
        });

        return builder;
    }
}