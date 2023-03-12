using Apsuite.Back.Application.Contract.Branch.Interface;
using Apsuite.Back.Application.Implement.Branch;
using Apsuite.Back.Domain.Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Application.Injection
{
    public static class DIExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }

        public static IServiceCollection ApplicationLayerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv)
        {
            Configuration = configuration;
            Services = services;
            WebEnv = webEnv;

            Services.DomainLayerServices(Configuration, WebEnv);

            // Branch
            Services.AddScoped<IBranchApplication, BranchApplication>();
            return services;
        }

        public static IApplicationBuilder UseApplicationLayerConfigure(this IApplicationBuilder app)
        {
            app.UseDomainLayerConfigure();

            return app;
        }
    }
}
