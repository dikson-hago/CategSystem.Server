using System.Text.Json;
using MlServer.Database.Repository;

using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class AddedObjectsValidator
{
    private readonly TableInfosRepository _tableInfosRepository;
    
    public AddedObjectsValidator(
        TableInfosRepository tableInfosRepository)
    {
        _tableInfosRepository = tableInfosRepository;
    }

    public async Task Validate(List<ContractDb.ObjectInfo> objectInfos, string tableName)
    {
        var tableInfo = 
            await _tableInfosRepository.GetTableInfo(tableName);

        string errors = "";
        
        foreach (var item in objectInfos)
        {
            var signsList = JsonSerializer.Deserialize<List<string>>(item.SignsInJson);
            
            if (tableInfo.ColumnsAmount != signsList.Count)
            {
                errors += $"Object with category {item.Category} has incorrect params";
            }
        }

        if (errors.Length != 0)
        {
            throw new Exception(errors);
        }

    }
}