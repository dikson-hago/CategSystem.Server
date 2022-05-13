using Microsoft.Extensions.DependencyInjection;
using MlServer.Application.Handlers;
using MlServer.Application.Handlers.Errors;
using MlServer.Application.Handlers.Handlers.DownloadTable;
using MlServer.Application.Handlers.Handlers.GetAllTablesInfos;
using MlServer.Application.Handlers.Handlers.GetTablesStatuses;
using MlServer.Orchestrator.Learners.Extensions;

namespace MlServer.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ApplicationHandlers>();
            
            services.AddHandlers();
            
            services.AddErrors();
            
            services.AddDistributors();
            
            services.AddDbOptions();
        }

        private static void AddHandlers(this IServiceCollection services)
        {
            services.AddObjectsHandler();
            services.AddTableHandler();
            
            services.AddSingleton<PredictCategoryHandler>();
            
            services.AddSingleton<RetrainHandler>();

            services.AddSingleton<GetTablesStatusesHandler>();

            services.AddSingleton<GetAllTablesInfosHandler>();

            services.AddSingleton<DownloadTableHandler>();
        }

        private static void AddObjectsHandler(this IServiceCollection services)
        {
            services.AddSingleton<AddObjectsHandler>();
            services.AddSingleton<AddedObjectsValidator>();
        }

        private static void AddTableHandler(this IServiceCollection services)
        {
            services.AddSingleton<AddTableHandler>();
            services.AddSingleton<AddedTableValidator>();
        }

        private static void AddErrors(this IServiceCollection services)
        {
            services.AddSingleton<ErrorsInCurrentSession>();
        }
    }
}