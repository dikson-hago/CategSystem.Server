using System.Text.Json;
using MlServer.Application.Handlers.Errors;
using MlServer.Application.Handlers.Mapper;
using MlServer.Orchestrator.Learners.Distriburtors;

using ContractML = MlServer.Contracts.Models.ML;
using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class PredictCategoryHandler
{
    private readonly PredictObjectDistributor _distributor;
    private ErrorsInCurrentSession _errors;

    
    public PredictCategoryHandler(
        PredictObjectDistributor distributor,
        ErrorsInCurrentSession errors)
    {
        _distributor = distributor;
    }

    public async Task<(List<ContractDb.ObjectInfo>, List<string>)> Handle(
        List<ContractML.ObjectCategory> objectInfos, string tableName)
    {
        var result = new List<ContractDb.ObjectInfo>();
        
        try
        {
            var predictions = await
                _distributor.Predict(objectInfos, tableName);

            for (int ind = 0; ind < predictions.Count; ind++)
            {
                result.Add(new ContractDb.ObjectInfo()
                {
                    Category = predictions[ind],
                    TableName = tableName,
                    SignsInJson = JsonSerializer.Serialize(objectInfos[ind].MapToList())
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