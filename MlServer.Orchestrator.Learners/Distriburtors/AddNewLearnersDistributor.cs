namespace MlServer.Orchestrator.Learners.Distriburtors;

public class AddNewLearnersDistributor
{
    public void AddNewLearner(string tableName)
    {
        TableLearnerCollection.SetNewLearner(tableName);
    }
}