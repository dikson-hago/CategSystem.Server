using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;

namespace MlServer.Client.Handlers;

internal class DownloadTableHandler : ClientHandlerBase
{
    public DownloadTableHandler(string url) : base(url)
    {
    }
    
    public async Task<List<MlObjectModel>> DownloadTable(string tableName)
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Greeter.GreeterClient(channel);

        var table = await Client.DownloadTableAsync(new DownloadTableRequest()
        {
            TableName = tableName
        });

        return table.MapToObjectsInfo();
    }
}