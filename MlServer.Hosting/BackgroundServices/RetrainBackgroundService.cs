using MlServer.Application.Handlers;
using MlServer.Database.Repository;
using MlServer.Orchestrator.Learners;

namespace MlServer.Hosting.BackgroundServices;

public class RetrainBackgroundService : BackgroundService
{
    private readonly RetrainTablesInfo _retrainTablesInfo;
    private readonly RetrainHandler _retrainHandler;
    private readonly TableInfosRepository _tableInfosRepository;

    public RetrainBackgroundService(
        RetrainHandler retrainHandler,
        TableInfosRepository tableInfosRepository)
    {
        _tableInfosRepository = tableInfosRepository;
        _retrainTablesInfo = RetrainTablesInfo.GetInstance();
        _retrainHandler = retrainHandler;
    }
    
    private async Task RetrainAlreadyExistedTables()
    {
        // get already added
        var allTableInfos = await
            _tableInfosRepository.GetAllTableNamesAndColumnsAmount();
            
        var allTableNames = 
            allTableInfos.Select(it => it.Key).ToList();
        
        foreach (var tableName in allTableNames)
        {
            TableLearnerCollection.StartFirstTrain(tableName);
        }

        foreach (var tableName in allTableNames)
        {
            await _retrainHandler.Retrain(tableName);
        }
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        await RetrainAlreadyExistedTables();
            
        while (!stoppingToken.IsCancellationRequested)
        {
            var tableName = _retrainTablesInfo.GetFirst();
            if (tableName != null)
            {
                await _retrainHandler.Retrain(tableName);
            }
        }
    }
}