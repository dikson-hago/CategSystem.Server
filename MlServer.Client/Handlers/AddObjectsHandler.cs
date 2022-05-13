using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;
//"http://localhost:5193"
namespace MlServer.Client.Handlers;

internal class AddObjectsHandler : ClientHandlerBase
{
    internal AddObjectsHandler(string url) : base(url)
    {
    }
    
    private ObjectInfo GetObjectInfo(MlObjectModel model)
    {
        var objectInfo = new ObjectInfo();
        
        objectInfo.TableName = model.TableName;
        objectInfo.Category = model.Category;
        objectInfo.Signs.AddRange(model.Signs);
        
        return objectInfo;
    }

    internal async Task<ErrorsCollection> AddObjects(List<MlObjectModel> objects)
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Greeter.GreeterClient(channel);

        var request = new AddObjectsRequest();
        request.Objects.Add(
            objects.Select(GetObjectInfo));

        var result = await Client.AddObjectsAsync(request);

        return result.MapToErrorsCollection();
    }
}