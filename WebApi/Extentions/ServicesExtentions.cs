using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.EFCore;
using Repositories.Contracts;

namespace WebApi.Extentions
{
    public static class ServicesExtentions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
            options.UseMySql(configuration.GetConnectionString("sqlConnection"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("sqlConnection"))));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

    }
}
