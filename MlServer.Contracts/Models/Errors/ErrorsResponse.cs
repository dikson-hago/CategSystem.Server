namespace MlServer.Contracts.Models;

public class ErrorsResponse
{
    public int ErrorsAmount { get; set; }
    
    public List<string> Errors { get; set; }
}