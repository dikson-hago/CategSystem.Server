namespace MlServer.Database.Models;

public class ObjectDbInfo
{
    public long Id { get; set; }
    public string TableName { get; set; }
    public string Category { get; set; }
    public string SignsInJson { get; set; }
}