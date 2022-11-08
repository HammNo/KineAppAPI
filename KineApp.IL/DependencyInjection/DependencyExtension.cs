using KineApp.BLL.Interfaces;
using KineApp.IL.Configurations;
using KineApp.IL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.IL.DependencyInjection
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, JwtConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }

        public static IServiceCollection AddMailer(this IServiceCollection services, MailerConfig config)
        {
            services.AddSingleton(config);
            services.AddScoped<SmtpClient>();
            services.AddScoped<IMailer, Mailer>();
            return services;
        }
    }
}
