using MlServer.Proto;

namespace MlServer.Client.Handlers.Base;

public abstract class ClientHandlerBase
{ 
    protected Greeter.GreeterClient Client { get; set; }

    protected readonly string Url;

    public ClientHandlerBase(string url)
    {
        Url = url;
    }
}
