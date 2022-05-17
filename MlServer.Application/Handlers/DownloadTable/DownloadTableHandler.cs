using MlServer.Application.Handlers.BaseHandlers;
using MlServer.Database.Repository;
using ContractDb = MlServer.Contracts.Models.Db;


namespace MlServer.Application.Handlers;

public class DownloadTableHandler : ObjectsInfosRepositoryBaseHandler
{
    private readonly TableInfosRepository _tableInfosRepository;
    
    public DownloadTableHandler(
        ObjectsInfosRepository repository,
        TableInfosRepository tableInfosRepository) 
        : base(repository)
    {
        _tableInfosRepository = tableInfosRepository;
    }
    
    public async Task<List<ContractDb.ObjectInfo>> Handler(string tableName)
    {
        var result = await Repository.GetTableByName(tableName);
        return result;
    }
}