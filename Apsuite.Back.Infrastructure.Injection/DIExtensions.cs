using Apsuite.Back.Infrastructure.Contract.Branch.Interface;
using Apsuite.Back.Infrastructure.Implement.Branch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Infrastructure.Injection
{
    public static class DIExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }

        public static IServiceCollection InfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv)
        {
            Configuration = configuration;
            Services = services;
            WebEnv = webEnv;

            // Security
            services.AddTransient<IBranchRepository, BranchRepository<SqlConnection>>(provider =>
            {
                BranchRepository<SqlConnection> result = new BranchRepository<SqlConnection>(
                    configuration: provider.GetRequiredService<IConfiguration>(),
                    connectionStringSection: "ConnectionString:Transactional"
                    , settingsSection: "BranchRepository");

                return result;
            });

            return services;
        }

        public static IApplicationBuilder UseInfrastructureLayerConfigure(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
