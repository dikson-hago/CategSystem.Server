using Microsoft.ML.Data;

namespace MlServer.Application.Models;

public class Prediction
{
    [ColumnName("PredictedLabel")] 
    public string Area;
}