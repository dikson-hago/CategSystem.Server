using System.Collections.Concurrent;

namespace MlServer.Orchestrator.Learners;

public class RetrainTablesInfo
{
    private static RetrainTablesInfo _instance = null;
    
    private ConcurrentQueue<string> RetrainTablesNames = new();

    private RetrainTablesInfo()
    {
    }

    public static RetrainTablesInfo GetInstance()
    {
        if (_instance is null)
        {
            _instance = new RetrainTablesInfo();
        }

        return _instance;
    }

    public void Add(string tableName)
    {
        RetrainTablesNames.Enqueue(tableName);
    }

    public string? GetFirst()
    {
        if (RetrainTablesNames.TryDequeue(out var tableName))
        {
            return tableName;
        }

        return null;
    }
}