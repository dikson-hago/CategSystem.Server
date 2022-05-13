using MlServer.Contracts.Models.Db;
using MlServer.Learners;
using MlServer.Orchestrator.Learners.Mapper;
using ContractMl = MlServer.Contracts.Models.ML;

namespace MlServer.Orchestrator.Learners.Distriburtors;

public class PredictObjectDistributor : MlDistributorBase
{
    public IMLLearner GetLearner(string tableName)
    {
        var learner = TableLearnerCollection.GetLearner(tableName);

        if (learner is null && !TableLearnerCollection.IsFirstTraining(tableName))
        {
            throw new Exception("Table is empty");
        }

        while (learner is null)
        {
            learner = TableLearnerCollection.GetLearner(tableName);
        }

        return learner;
    }
    
    public async Task<List<string>> Predict(
        List<ContractMl.ObjectCategory> objects, string tableName)
    {
        return await Task.Run(() =>
        {
            var result = new List<string>();

            var learner = GetLearner(tableName);

            FillEmptyCells(objects);
            
            foreach (var item in objects)
            {
                if (learner is null)
                {
                    result.Add("");
                }
                else
                {
                    result.Add(
                        learner.PredictCategory(item.MapToObjectCategory(false)));
                }
            }

            return result;
        }
        );
    }
}