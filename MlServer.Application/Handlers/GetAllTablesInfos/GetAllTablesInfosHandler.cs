using System.Text.Json;
using MlServer.Database.Repository;

using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class GetAllTablesInfosHandler
{
    private readonly TableInfosRepository _repository;

    public GetAllTablesInfosHandler(TableInfosRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ContractDb.TableInfo>> GetAllTablesInfos()
    {
        var tables = await _repository.GetAllTablesInfos();

        foreach (var table in tables)
        {
            var columnNames = JsonSerializer.Deserialize<List<string>>
                (table.ColumnNamesInJson);
            columnNames = columnNames?.Where(item => item.Length != 0).ToList();

            table.ColumnNamesInJson = JsonSerializer.Serialize(columnNames);
        }

        return tables;
    }
    
}