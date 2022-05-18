using System.Text.Json;
using MlServer.Application.Errors;
using MlServer.Database.Repository;
using MlServer.Orchestrator.Learners.Distriburtors;

using ContractML = MlServer.Contracts.Models.ML;
using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class PredictCategoryHandler
{
    private readonly PredictObjectDistributor _distributor;
    private readonly TableInfosRepository _repository;
    private ErrorsInCurrentSession _errors;

    
    public PredictCategoryHandler(
        PredictObjectDistributor distributor,
        ErrorsInCurrentSession errors,
        TableInfosRepository repository)
    {
        _distributor = distributor;
        _errors = errors;
        _repository = repository;
    }

    public async Task<(List<ContractDb.ObjectInfo>, List<string>)> Handle(
        List<ContractML.ObjectCategory> objectInfos, string tableName)
    {
        var result = new List<ContractDb.ObjectInfo>();
        
        try
        {
            var predictions = await
                _distributor.Predict(objectInfos, tableName);

            var currentTableColumnsAmount = await _repository.GetColumnsAmount(tableName);

            for (int ind = 0; ind < predictions.Count; ind++)
            {
                var columns = objectInfos[ind].MapToList();
                
                result.Add(new ContractDb.ObjectInfo()
                {
                    Category = predictions[ind],
                    TableName = tableName,
                    SignsInJson = JsonSerializer.Serialize(columns)
                });
            }
        }
        catch(Exception e)
        {
            _errors.AddError(e.Message);
        }

        return (result, _errors.GetErrors());
    }
}