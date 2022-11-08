using KineApp.BLL.Interfaces;
using KineApp.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KineApp.BLL.DependencyInjection
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IWeekService, WeekService>();
            services.AddScoped<ITimeSlotService, TimeSlotService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
