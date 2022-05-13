using Microsoft.ML.Data;

namespace MlServer.Learners.Models;

public class ObjectCategory
{

     [LoadColumn(0)]
     public string Sign1 { get; set; }
     
     [LoadColumn(1)]
     public string Sign2 { get; set; }
     
     [LoadColumn(2)]
     public string Sign3 { get; set; }
     
     [LoadColumn(3)]
     public string Sign4 { get; set; }
     
     [LoadColumn(4)]
     public string Sign5 { get; set; }

     [LoadColumn(5)]
     public string Category { get; set; }
     
}