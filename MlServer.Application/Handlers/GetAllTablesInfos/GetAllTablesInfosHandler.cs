using MlServer.Database.Repository;

using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers.Handlers.GetAllTablesInfos;

public class GetAllTablesInfosHandler
{
    private readonly TableInfosRepository _repository;

    public GetAllTablesInfosHandler(TableInfosRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ContractDb.TableInfo>> GetAllTablesInfos()
    {
        return await _repository.GetAllTablesInfos();
    }
    
}