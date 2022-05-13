namespace MlServer.Contracts.Models.Db;

public class TableInfo
{
    public string TableName { get; set; }
    
    public long ColumnsAmount { get; set; }
    
    public string CategoryColumnName { get; set; }
    
    public string ColumnNamesInJson { get; set; }
}