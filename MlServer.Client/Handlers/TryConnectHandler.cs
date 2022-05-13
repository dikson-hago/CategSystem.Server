using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Proto;
//"http://localhost:5193")

namespace MlServer.Client.Handlers;

public class TryConnectHandler : ClientHandlerBase
{
    public TryConnectHandler(string url) : base(url)
    {
    }

    internal async Task<bool> TryConnectAsync()
    {
        try
        {
            using var channel = GrpcChannel.ForAddress(Url);
            Client = new Greeter.GreeterClient(channel);

            await Client.TryConnectAsync(new Empty());

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}