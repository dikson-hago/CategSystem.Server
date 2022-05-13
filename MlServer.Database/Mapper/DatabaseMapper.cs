using System.Text.Json;
using MlServer.Database.Models;

using ContractMl = MlServer.Contracts.Models.ML;
using ContractDb = MlServer.Contracts.Models.Db;


namespace MlServer.Database.Mapper;

public static class DatabaseMapper
{
    public static ContractMl.ObjectCategory MapToContractMlObjectCategory(this ObjectDbInfo objectDbInfo)
    {
        var result = new ContractMl.ObjectCategory();
        
        var signs = JsonSerializer.Deserialize<List<string>>(objectDbInfo.SignsInJson);
        int signsIndex = 0;
        
        foreach (var prop in result.GetType().GetProperties())
        {
            if (prop.Name.Contains("Sign") && signs != null && signsIndex != signs.Count)
            {
                prop.SetValue(result, signs[signsIndex]);
                signsIndex++;
            }
            else if(prop.Name.Contains("Category"))
            {
                prop.SetValue(result, objectDbInfo.Category);
            }
            
        }

        return result;
    }

    public static List<ObjectDbInfo> MapToObjectDbInfo(this List<ContractDb.ObjectInfo> objectInfos)
    {
        return objectInfos.Select(item => new ObjectDbInfo()
        {
            TableName = item.TableName,
            Category = item.Category,
            SignsInJson = item.SignsInJson
        }).ToList();
    }

    public static List<ContractDb.ObjectInfo> MapToObjectInfo(this List<ObjectDbInfo> objectInfos)
    {
        var table = new List<ContractDb.ObjectInfo>();

        foreach (var item in objectInfos)
        {
            table.Add(new ContractDb.ObjectInfo()
            {
                Category = item.Category,
                TableName = item.TableName,
                SignsInJson = item.SignsInJson
            });
        }

        return table;
    }
    
}