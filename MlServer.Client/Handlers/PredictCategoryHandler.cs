using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;

namespace MlServer.Client.Handlers;

internal class PredictCategoryHandler : ClientHandlerBase
{
    internal PredictCategoryHandler(string url) : base(url)
    {
    }
    
    private PredictableObject GetPredictableObject(MlPredictableObject mlObject)
    {
        var result = new PredictableObject();
        result.TableName = mlObject.TableName;
        result.Signs.AddRange(mlObject.Signs);
        return result;
    }
    
    internal async Task<List<MlObjectModel>> PredictCategory(List<MlPredictableObject> objects)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5193");
        Client = new Greeter.GreeterClient(channel);

        var request = new PredictRequest();
        request.Objects.Add(
            objects.Select(GetPredictableObject));
        
        var result = await Client.PredictCategoryAsync(request);

        return result.MapToObjectsInfo();
    }
}