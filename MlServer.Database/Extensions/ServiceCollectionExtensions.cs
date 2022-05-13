using Microsoft.Extensions.DependencyInjection;
using MlServer.Database;
using MlServer.Database.Repository;

namespace MlServer.Services.Extensions {

    public static class ServiceCollectionExtensions
    {
        public static void AddDbOptions(this IServiceCollection services)
        {
            services.AddSingleton<MlServerDbContext>();
            services.AddRepositories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ObjectsInfosRepository>();
            services.AddSingleton<TableInfosRepository>();
        }
    }
}