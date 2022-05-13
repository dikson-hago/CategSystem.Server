using Microsoft.ML.Data;

namespace MlServer.Learners.LearnersAlgorythms.Sdca.Models;

public class ProductCategoryML
{
    [LoadColumn(0)]
    public string ID { get; set; }
    
    [LoadColumn(1)]
    public string ProductName { get; set; }
    
    [LoadColumn(2)]
    public string Category { get; set; }
}