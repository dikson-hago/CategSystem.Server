using Microsoft.ML.Data;

namespace MlServer.Learners.LearnersAlgorythms.Sdca.Models;

public class CategoryPredictionML
{
    [ColumnName("PredictedLabel")]
    public string Category { get; set; }
}