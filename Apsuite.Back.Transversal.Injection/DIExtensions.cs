using Apsuite.Back.Transversal.Contract.Global.Interface;
using Apsuite.Back.Transversal.Implement.Extensions;
using Apsuite.Back.Transversal.Implement.Toolbox;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Injection
{
    public static class DIExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }

        public static IHostBuilder TuApWebApiHost<TStartup>(this IHostBuilder hostBuilder, Assembly assambly) where TStartup : class
        {
            hostBuilder.WebApiHost<TStartup>(assambly);

            return hostBuilder;
        }

        public static IServiceCollection TransversalLayerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv)
        {
            Configuration = configuration;
            Services = services;
            WebEnv = webEnv;

            services.WebApiServices(Configuration, WebEnv);

            Services.AddSingleton<ICipher, Cipher>();

            return services;
        }

        public static IApplicationBuilder UseTransversalLayerConfigure(this IApplicationBuilder app)
        {
            app.UseWebApiConfigure();

            return app;
        }
    }
}
