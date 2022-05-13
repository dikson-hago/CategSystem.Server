// using Microsoft.ML;
// using Microsoft.ML.Trainers;
// using MlServer.Application.Learners.LongLearner.Paths;
// using MlServer.Application.Learners.Models.Products;
//
// namespace MlServer.Application.Learners.LongLearner;
//
// public class MlFileMultiLearner
// {
//     private MLContext _mlContext;
//     private ITransformer _trainedModel;
//     private PredictionEngine<ProductCategory, CategoryPrediction> _predEngine;
//     IEstimator<ITransformer> _pipeline;
//
//     public MlFileMultiLearner()
//     {
//         _mlContext = new MLContext(0);
//         _trainedModel = null;
//     }
//
//     public void Train(IEnumerable<ProductCategory> data)
//     {
//         // load data view for training
//         IDataView dataForTrainingDataView = 
//             LoadDataForTraining(data);
//
//         InitPipeline();
//         
//         StartTrainModelAndSaveResults(dataForTrainingDataView);
//         
//         //SaveModelAsFile(dataForTrainingDataView.Schema);
//     }
//
//     private LbfgsMaximumEntropyMulticlassTrainer? GetCurrentLearningAlgorithm()
//     {
//         return _mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy();
//     }
//
//     private IDataView LoadDataForTraining(IEnumerable<ProductCategory> data)
//     {
//         return _mlContext.Data.LoadFromEnumerable<ProductCategory>(data);
//     }
//
//     private void InitPipeline()
//     {
//         // init pipeline
//         _pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Category", outputColumnName: "Label")
//                 .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "ProductName", outputColumnName: "ProductNameFeaturized"))
//                 .Append(_mlContext.Transforms.Concatenate("Features", "ProductNameFeaturized"));
//     }
//
//     private void StartTrainModelAndSaveResults(IDataView dataForTrainingDataView)
//     {
//         // init pipeline for model building
//         var dataPrepEstimator = 
//             _pipeline.Append(
//                 _mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
//         
//         ITransformer dataPrepTransformer = dataPrepEstimator.Fit(dataForTrainingDataView);
//         
//         IDataView transformedData = dataPrepTransformer.Transform(dataForTrainingDataView);
//         
//         TrainModel(transformedData);
//         
//         SaveModelsToFiles(dataPrepTransformer, dataForTrainingDataView, transformedData);
//     }
//
//     private void TrainModel(IDataView dataView)
//     {
//         _trainedModel = GetCurrentLearningAlgorithm()?.Fit(dataView);
//     }
//
//     public void Retrain(IEnumerable<ProductCategory> data)
//     {
//         DataViewSchema dataPrepPipelineSchema, modelSchema;
//         
//         ITransformer dataPrepPipeline = 
//             _mlContext.Model.Load(FilePaths.SaveMlModelPath, out dataPrepPipelineSchema);
//         
//         ITransformer trainedModel = _mlContext.Model.Load(FilePaths.SaveDataPrepTransformerPath, out modelSchema);
//
//         IDataView newDataForTraining = _mlContext.Data.LoadFromEnumerable(data);
//
//         IDataView transformedNewDataForTraining = dataPrepPipeline.Transform(newDataForTraining);
//         
//         var learnAlgorithm = _mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy();
//         
//         MaximumEntropyModelParameters? modelParameters = ((ISingleFeaturePredictionTransformer<object>)trainedModel).Model
//             as MaximumEntropyModelParameters;
//
//         _trainedModel = learnAlgorithm.Fit(transformedNewDataForTraining, modelParameters);
//     }
//
//     private void SaveModelsToFiles(ITransformer dataPrepTransformer, 
//         IDataView dataForTrainingDataView, IDataView transformedData)
//     {
//         _mlContext.Model.Save(dataPrepTransformer, dataForTrainingDataView.Schema, FilePaths.SaveDataPrepTransformerPath);
//         _mlContext.Model.Save(_trainedModel, transformedData.Schema, FilePaths.SaveMlModelPath);
//     }
//
//     public string Predict(ProductCategory productCategory) {
//         // load saved model into application
//         /*ITransformer loadedModel = 
//             _mlContext.Model.Load(FilePaths.SaveMlModelPath, out var modelInputSchema);*/
//
//         // prepare for prediction by _predEngine
//         _predEngine = 
//             _mlContext.Model
//                 .CreatePredictionEngine<ProductCategory, CategoryPrediction>(_trainedModel);
//         
//         // get prediction
//         var prediction = _predEngine.Predict(productCategory);
//
//         return prediction.Category;
//     }
// }