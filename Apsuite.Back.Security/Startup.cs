using Apsuite.Back.Application.Injection;
using Apsuite.Back.Transversal.Injection;
using NLog;
using System;
using System.Reflection;

namespace Apsuite.Back.Service.Security
{
    public class Startup
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webEnv)
        {
            LogManager.LoadConfiguration(string.Concat(AppContext.BaseDirectory, "/Setting/nlog.config"));
            Configuration = configuration;
            WebEnv = webEnv;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Services = services;
            Services.TransversalLayerServices(Configuration!, WebEnv!);
            Services.ApplicationLayerServices(Configuration!, WebEnv!);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseTransversalLayerConfigure();
            app.UseApplicationLayerConfigure();
        }
    }
}
