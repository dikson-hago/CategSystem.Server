using MlServer.Contracts.Errors;
using MlServer.Contracts.Models.TablesStatuses;
using ContractMl = MlServer.Contracts.Models.ML;
using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class ApplicationHandlers
{
    private readonly PredictCategoryHandler _predictCategoryHandler;
    private readonly AddTableHandler _addTableHandler;
    private readonly AddObjectsHandler _addObjectHandler;
    private readonly GetTablesStatusesHandler _getTablesStatusesHandler;
    private readonly GetAllTablesInfosHandler _getAllTablesInfosHandler;
    private readonly DownloadTableHandler _downloadTableHandler;

    public ApplicationHandlers(
        PredictCategoryHandler predictCategoryHandler, 
        AddObjectsHandler addObjectHandler, 
        AddTableHandler addTableHandler,
        GetTablesStatusesHandler tablesStatusesHandler,
        GetAllTablesInfosHandler getAllTablesInfosHandler,
        DownloadTableHandler downloadTableHandler)
    { 
        _predictCategoryHandler = predictCategoryHandler;
        _addObjectHandler = addObjectHandler; 
        _addTableHandler = addTableHandler;
        _getTablesStatusesHandler = tablesStatusesHandler;
        _getAllTablesInfosHandler = getAllTablesInfosHandler;
        _downloadTableHandler = downloadTableHandler;
    }

    public async Task<(List<ContractDb.ObjectInfo>, List<string>)> PredictCategory(
        List<ContractMl.ObjectCategory> objectInfos, string tableName)
    { 
       return await _predictCategoryHandler.Handle(objectInfos, tableName);
    }

    public async Task<ErrorsResponse> AddObjects(List<ContractDb.ObjectInfo> objectInfos,
        string tableName)
    {
        return await _addObjectHandler.Handle(objectInfos, tableName);
    }
    
    public async Task<ErrorsResponse> AddTable(ContractDb.TableInfo tableInfo)
    {
        return await _addTableHandler.Handle(tableInfo);
    }

    public List<TableStatus> GetTablesStatuses()
    {
        return _getTablesStatusesHandler.Handle();
    }

    public async Task<List<ContractDb.TableInfo>> GetAllTablesInfos()
    {
        return await _getAllTablesInfosHandler.GetAllTablesInfos();
    }

    public async Task<List<ContractDb.ObjectInfo>> DownloadTable(string tableName)
    {
        return await _downloadTableHandler.Handler(tableName);
    }
}