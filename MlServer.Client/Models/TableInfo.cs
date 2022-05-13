namespace MlServer.Client.Models;

public class TableInfo
{
    public string TableName { get; set; }
    
    public string CategoryColumnName { get; set; }
    
    public List<string> ColumnNames { get; set; }
}