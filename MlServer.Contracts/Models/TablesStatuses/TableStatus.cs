namespace MlServer.Contracts.Models.TablesStatuses;

public class TableStatus
{
    public string TableName { get; set; }
    
    public TableStatusEnum Status { get; set; }
}