using Microsoft.Extensions.DependencyInjection;
using waterfood.Core.Services;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Core.DependencyInjections
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IReserveService, ReserveService>();
            
            return services;
        }
    }
}
