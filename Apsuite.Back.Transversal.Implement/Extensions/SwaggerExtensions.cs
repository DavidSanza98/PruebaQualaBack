using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Implement.Extensions
{
    public static class SwaggerExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }
        public static Assembly? Assembly { get; private set; }

        public static IServiceCollection SwaggerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv, Assembly assembly)
        {
            Configuration = configuration;
            Services = services;
            Assembly = assembly;
            WebEnv = webEnv;

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TuAp API by Xolit",
                    Description = "This is the documentation to this API",
                    TermsOfService = new Uri("https://exampletuap.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Hydra group by Xolit",
                        Email = "hydra@Xolit.com",
                        Url = new Uri("https://exampletuap.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under COPYRIGHT licence",
                        Url = new Uri("https://exampletuap.com/licence")
                    }
                });

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Authorization by API key.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer {JWT}"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });

                setup.UseAllOfToExtendReferenceSchemas();
                setup.UseInlineDefinitionsForEnums();

                setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetName().Name}.xml"));
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger(setup =>
            {
                setup.RouteTemplate = "api-docs/{documentname}.json";
            });

            app.UseSwaggerUI(setup =>
            {
                setup.ConfigObject = new ConfigObject { ShowCommonExtensions = true };
                setup.SwaggerEndpoint("/api-docs/v1.json", "TuAp V1");
                setup.DocumentTitle = "TuAp API Documentation";
                setup.DocExpansion(DocExpansion.None);
                setup.EnableDeepLinking();
                setup.RoutePrefix = "api-docs";
            });

            return app;
        }
    }
}
