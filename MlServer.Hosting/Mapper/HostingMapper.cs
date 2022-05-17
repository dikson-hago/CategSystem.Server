using System.Text.Json;
using MlServer.Contracts.Errors;
using MlServer.Contracts.Models.TablesStatuses;
using MlServer.Proto;

using ContractsDb = MlServer.Contracts.Models.Db;
using ContractsMl = MlServer.Contracts.Models.ML;

namespace MlServer.Hosting.Mapper;

public static class HostingMapper
{
    public static ContractsDb.TableInfo MapToTableInfo(this AddTableRequest request)
    {
        var result = new ContractsDb.TableInfo()
        {
            TableName = request.TableName,
            ColumnsAmount = request.ColumnNames.Count(item => item != ""),
            CategoryColumnName = request.CategoryColumnName,
            ColumnNamesInJson = JsonSerializer.Serialize(request.ColumnNames)
        };

        return result;
    }

    public static List<ContractsDb.ObjectInfo> MapToObjectsInfos(this AddObjectsRequest request)
    {
        var result = new List<ContractsDb.ObjectInfo>();

        foreach (var item in request.Objects)
        {
            result.Add(new ContractsDb.ObjectInfo()
            {
                TableName = request.TableName,
                Category = item.Category,
                SignsInJson = JsonSerializer.Serialize(item.Signs)
            });
        }

        return result;
    }

    public static List<ContractsMl.ObjectCategory> MapToObjectsCategoryList(this PredictRequest request)
    {
        var result = new List<ContractsMl.ObjectCategory>();

        foreach (var item in request.Objects)
        {
            var objectForPrediction = new ContractsMl.ObjectCategory();

            var signs = item.Signs.ToList();
            int index = 0;
            foreach (var prop in objectForPrediction.GetType().GetProperties())
            {
                if (prop.Name.Contains("Sign") && index < signs.Count)
                {
                    prop.SetValue(objectForPrediction, signs[index]);
                    index++;
                }
            }
            
            result.Add(objectForPrediction);
        }

        return result;

    }
    
    public static ErrorsResultResponse MapToErrorsResultResponse(this ErrorsResponse errors)
    {
        var result = new ErrorsResultResponse()
        {
            ErrorsAmount = errors.ErrorsAmount
        };
        
        result.Errors.AddRange(errors.Errors);

        return result;
    }

    public static GetTablesStatusesResponse MapToGetTablesStatusesResponse(this List<TableStatus> tableStatusList)
    {
        var result = new GetTablesStatusesResponse();

        foreach (var tableInfo in tableStatusList)
        {
            result.TablesStatuses.Add(new TableStatusModel()
            {
                Status = tableInfo.Status.ToString(),
                TableName = tableInfo.TableName
            });
        }

        return result;
    }

    public static GetAllTablesResponse MapToGetAllTablesResponse(this List<ContractsDb.TableInfo> tableInfos)
    {
        var result = new GetAllTablesResponse()
        {
            TableInfo = {  }
        };

        foreach (var tableInfo in tableInfos)
        {
            var columnNames = JsonSerializer.Deserialize<List<string>>(tableInfo.ColumnNamesInJson);
            var model = new TableInfo();

            model.TableName = tableInfo.TableName;
            model.CategoryColumnName = tableInfo.CategoryColumnName;
            model.ColumnNames.AddRange(columnNames);

            result.TableInfo.Add(model);
        }

        return result;
    }

    public static GetObjectsResponse MapToObjectsResponse(this (List<ContractsDb.ObjectInfo>, List<string>) objects)
    {
        var result = new GetObjectsResponse();

        if (objects.Item2.Count != 0)
        {
            result.Errors.AddRange(objects.Item2);
            return result;
        }

        foreach (var element in objects.Item1)
        {
            var objectInfo = new Proto.ObjectInfo();
            objectInfo.Category = element.Category;
            var signs = JsonSerializer.Deserialize<List<string>>(element.SignsInJson);
            objectInfo.Signs.AddRange(signs);

            result.Objects.Add(objectInfo);
        }

        return result;
    }
    
    public static GetObjectsResponse MapToObjectsResponse(this List<ContractsDb.ObjectInfo> objects)
    {
        var result = new GetObjectsResponse();
        
        foreach (var element in objects)
        {
            var objectInfo = new Proto.ObjectInfo();
            objectInfo.Category = element.Category;
            var signs = JsonSerializer.Deserialize<List<string>>(element.SignsInJson);
            objectInfo.Signs.AddRange(signs);

            result.Objects.Add(objectInfo);
        }

        return result;
    }
    
}