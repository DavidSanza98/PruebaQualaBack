using Apsuite.Back.Domain.Contract.Branch.Interface;
using Apsuite.Back.Domain.Implement.Branch;
using Apsuite.Back.Infrastructure.Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Domain.Injection
{
    public static class DIExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }

        public static IServiceCollection DomainLayerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv)
        {
            Configuration = configuration;
            Services = services;
            WebEnv = webEnv;

            Services.InfrastructureLayerServices(Configuration, WebEnv);

            // Branch
            Services.AddScoped<IBranchDomain, BranchDomain>();

            return Services;
        }

        public static IApplicationBuilder UseDomainLayerConfigure(this IApplicationBuilder app)
        {
            app.UseInfrastructureLayerConfigure();

            return app;
        }
    }
}
