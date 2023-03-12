using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Implement.Extensions
{
    public static class WebApiExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }
        public static Assembly? Assembly { get; private set; }

        public static IHostBuilder WebApiHost<TStartup>(this IHostBuilder hostBuilder, Assembly assembly) where TStartup : class
        {
            Assembly = assembly;

            hostBuilder
                .ConfigureAppConfiguration((builder, config) =>
                {
                    IConfiguration configuration = builder.Configuration;
                    IHostEnvironment hostEnv = builder.HostingEnvironment;

                    hostEnv.ApplicationName = assembly.GetName().Name;
                    string name = hostEnv.EnvironmentName;

                    config.SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile($"Setting/{name}.jsonc", false, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((context, config) => config.ClearProviders())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<TStartup>());

            return hostBuilder;
        }

        public static IServiceCollection WebApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv)
        {
            Configuration = configuration;
            Services = services;
            WebEnv = webEnv;

            Services.AddCors();

            Services.AddMvcCore().AddApiExplorer();
            Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddApplicationPart(Assembly!)
                //.SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddControllersAsServices()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((Host) => true)
                    .AllowCredentials();
                });
            });

            Services.AddMemoryCache();

            Services.JwtServices(configuration, WebEnv, Assembly!);
            Services.SwaggerServices(configuration, WebEnv, Assembly!);

            return services;
        }

        public static IApplicationBuilder UseWebApiConfigure(this IApplicationBuilder app)
        {
            if (WebEnv.IsEnvironment("Localhost")) app.UseDeveloperExceptionPage();
            else if (WebEnv.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("AllowOrigin");

            app.UseRouting()
                .UseJwtConfigure()
                .UseSwaggerConfigure();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
