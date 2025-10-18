using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add your persistence related service registrations here
            services.AddDbContext<HrDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
            });
            return services;
        }
    }
}
