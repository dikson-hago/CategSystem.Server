using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;

namespace MlServer.Client.Handlers;

internal class GetTablesStatusesHandler : ClientHandlerBase
{
    internal GetTablesStatusesHandler(string url) : base(url)
    {
    }

    internal async Task<List<TableStatusInfo>> GetTablesStatusesInfo()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5193");
        Client = new Greeter.GreeterClient(channel);

        var result = await Client.GetTablesStatusesAsync(new Empty());

        return result.MapToTableStatusInfoList();
    }
}