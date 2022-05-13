using MlServer.Contracts.Models.TablesStatuses;
using MlServer.Orchestrator.Learners;

namespace MlServer.Application.Handlers.Handlers.GetTablesStatuses;

public class GetTablesStatusesHandler
{
    public GetTablesStatusesHandler()
    {
    }

    public List<TableStatus> Handle()
    {
        var result = new List<TableStatus>();
        
        var tableInfos = TableLearnerCollection.GetLearnersInfo();

        foreach (var info in tableInfos)
        {
            if (info.Value is null)
            {
                result.Add(new TableStatus()
                {
                    Status = TableStatusEnum.Loading,
                    TableName = info.Key
                });
            }
            else
            {
                result.Add(new TableStatus()
                {
                    Status = TableStatusEnum.Connected,
                    TableName = info.Key
                });
            }
        }

        return result;
    }
}