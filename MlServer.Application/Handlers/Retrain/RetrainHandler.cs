
using MlServer.Database.Repository;
using MlServer.Orchestrator.Learners.Distriburtors;

namespace MlServer.Application.Handlers;

public class RetrainHandler
{
    private readonly RetrainDistributor _distributor;
    private readonly ObjectsInfosRepository _repository;
    
    public RetrainHandler(RetrainDistributor distributor,
        ObjectsInfosRepository repository)
    {
        _distributor = distributor;
        _repository = repository;
    }
    
    public async Task Retrain(string tableName)
    {
        var list = await _repository.GetObjectCategoryList(tableName);
        await _distributor.Retrain(tableName, list);
    }
}