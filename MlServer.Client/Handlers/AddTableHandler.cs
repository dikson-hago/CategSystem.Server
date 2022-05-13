using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;
using TableInfo = MlServer.Client.Models.TableInfo;

namespace MlServer.Client.Handlers;

internal class AddTableHandler : ClientHandlerBase
{
    internal AddTableHandler(string url) : base(url)
    {
    }
    
    internal async Task<ErrorsCollection> AddTable(TableInfo tableInfo)
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Greeter.GreeterClient(channel);

        var request = new AddTableRequest();

        request.TableName = tableInfo.TableName;
        request.CategoryColumnName = tableInfo.CategoryColumnName;
        request.ColumnNames.AddRange(tableInfo.ColumnNames);

        var result = await Client.AddTableAsync(request);

        return result.MapToErrorsCollection();
    }
}