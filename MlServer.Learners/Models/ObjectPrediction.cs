using Microsoft.ML.Data;

namespace MlServer.Learners.Models;

public class ObjectPrediction
{
    [ColumnName("PredictedLabel")]
    public string Category;
}