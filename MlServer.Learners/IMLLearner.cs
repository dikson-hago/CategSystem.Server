using MlServer.Learners.Models;

namespace MlServer.Learners;

public interface IMLLearner
{ 
    Task Learn(IEnumerable<ObjectCategory> data);

    string PredictCategory(ObjectCategory numberModel);

    bool IsTrained();
}