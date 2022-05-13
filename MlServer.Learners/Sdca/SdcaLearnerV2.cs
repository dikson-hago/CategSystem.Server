using Microsoft.ML;
using Microsoft.ML.Data;
using MlServer.Learners.Models;

namespace MlServer.Learners.LearnersAlgorythms.SdcaMaximumEntropyAlgorythm;

public class SdcaLearnerV2 : IMLLearner
{
    private MLContext _mlContext;
    private ITransformer _trainedModel;
    private PredictionEngine<ObjectCategory, ObjectPrediction> _predEngine;
    private bool _isTrained;

    public bool IsTrained() => _isTrained;

    public SdcaLearnerV2()
    {
        _mlContext = new MLContext(0);
        _trainedModel = null;
        _isTrained = false;
    }

    public async Task Learn(IEnumerable<ObjectCategory> data)
    {
        await Task.Run(() =>
        {
            // create data view for training
            IDataView trainingDataView =
                _mlContext.Data.LoadFromEnumerable<ObjectCategory>(data);

            // init pipeline
            var pipeline = CreatePipeline();

            var trainingPipeline = BuildAndTrainModel(trainingDataView, pipeline);

            _isTrained = true;
        });

        //SaveModelAsFile(trainingDataView.Schema);
    }

    private EstimatorChain<ColumnConcatenatingTransformer> CreatePipeline()
    {
        var pipeline = _mlContext.Transforms.Conversion
            .MapValueToKey(inputColumnName: "Category", outputColumnName: "Label")
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Sign1",
                outputColumnName: "SignFeaturized1"))
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Sign2",
                outputColumnName: "SignFeaturized2"))
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Sign3",
                outputColumnName: "SignFeaturized3"))
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Sign4",
                outputColumnName: "SignFeaturized4"))
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Sign5",
                outputColumnName: "SignFeaturized5"))
            .Append(_mlContext.Transforms.Concatenate("Features",
                "SignFeaturized1", "SignFeaturized2", "SignFeaturized3", "SignFeaturized4", "SignFeaturized5"))
            .AppendCacheCheckpoint(_mlContext);

        return pipeline;
    }

    private IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
    {
        var trainingPipeline = 
            pipeline.Append(
                    _mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        
        _trainedModel = trainingPipeline.Fit(trainingDataView);
        
        _predEngine = 
            _mlContext.Model.CreatePredictionEngine<ObjectCategory, ObjectPrediction>(_trainedModel);

        return trainingPipeline;
    }

    // private void SaveModelAsFile(DataViewSchema trainingDataViewSchema)
    // {
    //     _mlContext.Model.Save(_trainedModel, trainingDataViewSchema, FilePaths.SaveMlModelPath);
    // }
    
    public string PredictCategory(ObjectCategory numberModel)
    {
        // ITransformer loadedModel = _mlContext.Model.Load(FilePaths.SaveMlModelPath, out var modelInputSchema);
        // _predEngine = _mlContext.Model.CreatePredictionEngine<ProductCategory, CategoryPrediction>(loadedModel);

        _predEngine = _mlContext.Model.CreatePredictionEngine<ObjectCategory, ObjectPrediction>(_trainedModel);

        var prediction = _predEngine.Predict(numberModel);

        return prediction.Category;
    }
}