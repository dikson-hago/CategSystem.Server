using System.Collections.Concurrent;
using MlServer.Learners;

namespace MlServer.Orchestrator.Learners;

public static class TableLearnerCollection
{
    private static readonly ConcurrentDictionary<string, IMLLearner?> Learners = new();
    private static ConcurrentDictionary<string, bool> FirstTrainTables = new();

    public static void SetNewLearner(string tableName)
    {
        Learners.TryAdd(tableName, null);
    }

    public static void StartFirstTrain(string tableName)
    {
        FirstTrainTables[tableName] = true;
        Learners[tableName] = null;
    }

    public static void UpdateOrStopTraining(string tableName, IMLLearner? learner)
    {
        FirstTrainTables[tableName] = false;
        Learners[tableName] = learner;
    }

    public static bool IsFirstTraining(string tableName)
    {
        return FirstTrainTables[tableName] == true;
    }
    
    public static IMLLearner? GetLearner(string tableName)
    {
        Learners.TryGetValue(tableName, out IMLLearner? value);
        return value;
    }

    public static ConcurrentDictionary<string, IMLLearner?> GetLearnersInfo()
    {
        return new ConcurrentDictionary<string, IMLLearner?>(Learners);
    }
}