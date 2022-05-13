using MlServer.Database.Repository;

using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class AddedTableValidator
{
    private readonly TableInfosRepository _tableInfosRepository;
    
    public AddedTableValidator(
        TableInfosRepository tableInfosRepository)
    {
        _tableInfosRepository = tableInfosRepository;
    }

    public async Task Validate(ContractDb.TableInfo tableInfo)
    {
        if (await _tableInfosRepository.EnsureTableExist(tableInfo.TableName))
        {
            throw new Exception("Current table exists yet");
        }
    }
}