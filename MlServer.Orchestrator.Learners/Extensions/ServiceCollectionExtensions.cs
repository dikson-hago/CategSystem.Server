using Microsoft.Extensions.DependencyInjection;
using MlServer.Orchestrator.Learners.Distriburtors;

namespace MlServer.Orchestrator.Learners.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDistributors(this IServiceCollection services)
    {
        services.AddSingleton<PredictObjectDistributor>();
        
        services.AddSingleton<AddNewLearnersDistributor>();

        services.AddSingleton<RetrainDistributor>();
    }
}