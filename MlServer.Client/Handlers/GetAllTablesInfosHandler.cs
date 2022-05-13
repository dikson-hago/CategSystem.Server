using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;

using Contract = MlServer.Client.Models;

namespace MlServer.Client.Handlers;

internal class GetAllTablesInfosHandler : ClientHandlerBase
{
    internal GetAllTablesInfosHandler(string url) : base(url)
    {
    }
    
    internal async Task<List<Contract.TableInfo>> GetTablesStatusesInfo()
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Greeter.GreeterClient(channel);

        var result = await Client.GetAllTablesInfosAsync(new Empty());

        return result.MapToTableInfo();
    }
}