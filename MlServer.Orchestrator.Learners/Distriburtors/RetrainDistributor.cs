using MlServer.Learners.LearnersAlgorythms.SdcaMaximumEntropyAlgorythm;
using MlServer.Orchestrator.Learners.Mapper;
using ContractMl = MlServer.Contracts.Models.ML;

namespace MlServer.Orchestrator.Learners.Distriburtors;

public class RetrainDistributor : MlDistributorBase
{
    public RetrainDistributor()
    { }

    public async Task Retrain(string tableName, IEnumerable<ContractMl.ObjectCategory> data)
    {
        var learner = new SdcaLearnerV2();

        if (!data.ToList().Any())
        {
            learner = null;
        }
        else
        {
            FillEmptyCells(data);
            await learner.Learn(data.MapToIEnumObjCategoryForLearn());
        }
        TableLearnerCollection.UpdateOrStopTraining(tableName, learner);
    }
}