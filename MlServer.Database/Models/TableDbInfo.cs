namespace MlServer.Database.Models;

public class TableDbInfo
{
    public long Id { get; set; }
    public string TableName { get; set; }
    public long ColumnsAmount { get; set; }
    
    public string CategoryColumnName { get; set; }
    
    public string ColumnNames { get; set; }
}