using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Implement.Extensions
{
    public static class JwtExtensions
    {
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection? Services { get; private set; }
        public static IWebHostEnvironment? WebEnv { get; private set; }
        public static Assembly? Assembly { get; private set; }
        private static Logger logger = LogManager.GetLogger("LogTokenSession");

        public static IServiceCollection JwtServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webEnv, Assembly assembly)
        {
            Configuration = configuration;
            Services = services;
            Assembly = assembly;
            WebEnv = webEnv;

            string issuer = Configuration["Jwt:Issuer"]!;
            byte[] key = Encoding.ASCII.GetBytes(Configuration["Jwt:Keypass"]!);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Assembly.GetName().Name,
                    LifetimeValidator = LifetimeValidator,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    }
                };

            });

            return Services;
        }

        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                if (expires > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    logger.Warn($"Token: {token}, TimeExpire: {expires}, TimeExpireComp: {DateTime.Now} ");
                    return false;
                }
            }
            return false;
        }

        public static IApplicationBuilder UseJwtConfigure(this IApplicationBuilder app)
        {
            app.UseAuthentication()
                .UseAuthorization();

            return app;
        }
    }
}
