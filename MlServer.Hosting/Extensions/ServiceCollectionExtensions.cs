using Microsoft.EntityFrameworkCore;
using MlServer.Application.Handlers;
using MlServer.Database;
using MlServer.Hosting.BackgroundServices;

public static class ServiceCollectionExtensions
{
    public static void AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        // получаем строку подключения из файла конфигурации
        string connection = configuration.GetConnectionString("DefaultConnection");
        // добавляем контекст ApplicationContext в качестве сервиса в приложение
        services.AddDbContextFactory<MlServerDbContext>(options =>
            options.UseNpgsql(connection));
    }

    public static void AddRetrainBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<RetrainBackgroundService>();
    }
}