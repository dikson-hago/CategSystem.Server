using MlServer.Database.Repository;

namespace MlServer.Application.Handlers.BaseHandlers;

public abstract class ObjectsInfosRepositoryBaseHandler
{
    protected readonly ObjectsInfosRepository Repository;

    public ObjectsInfosRepositoryBaseHandler(ObjectsInfosRepository repository)
    {
        Repository = repository;
    }
}