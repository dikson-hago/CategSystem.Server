using Microsoft.ML;
using MlServer.Learners.LearnersAlgorythms.Sdca.Models;
using MlServer.Learners.Paths;

namespace MlServer.Learners.LearnersAlgorythms.SdcaMaximumEntropyAlgorythm;

public class SdcaLearner
{
    private MLContext _mlContext;
    private ITransformer _trainedModel;
    private PredictionEngine<ProductCategoryML, CategoryPredictionML> _predEngine;
    
    public SdcaLearner()
    {
        _mlContext = new MLContext(0);
        _trainedModel = null;
    }

    public async Task Learn(IEnumerable<ProductCategoryML> data)
    {
        await Task.Run(() =>
        {
            // create data view for training
            IDataView trainingDataView =
                _mlContext.Data.LoadFromEnumerable<ProductCategoryML>(data);

            // init pipeline
            var pipeline = _mlContext.Transforms.Conversion
                .MapValueToKey(inputColumnName: "Category", outputColumnName: "Label")

                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "ProductName",
                    outputColumnName: "ProductNameFeaturized"))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    "ProductNameFeaturized"))
                .AppendCacheCheckpoint(_mlContext);

            var trainingPipeline = BuildAndTrainModel(trainingDataView, pipeline);
        });

        //SaveModelAsFile(trainingDataView.Schema);
    }
    private IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
    {
        var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        
        _trainedModel = trainingPipeline.Fit(trainingDataView);
        
        _predEngine = _mlContext.Model.CreatePredictionEngine<ProductCategoryML, CategoryPredictionML>(_trainedModel);

        return trainingPipeline;
    }

    private void SaveModelAsFile(DataViewSchema trainingDataViewSchema)
    {
        _mlContext.Model.Save(_trainedModel, trainingDataViewSchema, FilePaths.SaveMlModelPath);
    }
    public async Task<string> PredictGroup(ProductCategoryML numberModel)
    {
        return await Task.Run(() =>
        {
            // ITransformer loadedModel = _mlContext.Model.Load(FilePaths.SaveMlModelPath, out var modelInputSchema);

            // _predEngine = _mlContext.Model.CreatePredictionEngine<ProductCategory, CategoryPrediction>(loadedModel);

            _predEngine = _mlContext.Model.CreatePredictionEngine<ProductCategoryML, CategoryPredictionML>(_trainedModel);

            var prediction = _predEngine.Predict(numberModel);

            return prediction.Category;
        });
    }
}
