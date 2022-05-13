using MlServer.Client.Handlers;
using MlServer.Client.Models;

namespace MlServer.Client;

public class MlServerApi
{
    private readonly AddTableHandler _addTableHandler;
    private readonly AddObjectsHandler _addObjectsHandler;
    private readonly PredictCategoryHandler _predictCategoryHandler;
    private readonly GetTablesStatusesHandler _getTablesStatusesHandler;
    private readonly GetAllTablesInfosHandler _getAllTablesInfosHandler;
    private readonly TryConnectHandler _tryConnectHandler;
    private readonly DownloadTableHandler _downloadTableHandler;

    public MlServerApi(string url)
    {
        _addTableHandler = new AddTableHandler(url);
        _addObjectsHandler = new AddObjectsHandler(url);
        _predictCategoryHandler = new PredictCategoryHandler(url);
        _getTablesStatusesHandler = new GetTablesStatusesHandler(url);
        _getAllTablesInfosHandler = new GetAllTablesInfosHandler(url);
        _tryConnectHandler = new TryConnectHandler(url);
        _downloadTableHandler = new DownloadTableHandler(url);
    }

    public async Task<ErrorsCollection> AddTable(TableInfo tableInfo)
    {
        return await _addTableHandler.AddTable(tableInfo);
    }

    public async Task<ErrorsCollection> AddObjects(List<MlObjectModel> objects)
    {
        return await _addObjectsHandler.AddObjects(objects);
    }

    public async Task<List<MlObjectModel>> PredictCategory(List<MlPredictableObject> objects)
    {
        return await _predictCategoryHandler.PredictCategory(objects);
    }

    public async Task<List<TableStatusInfo>> GetTablesStatusesInfo()
    {
        return await _getTablesStatusesHandler.GetTablesStatusesInfo();
    }

    public async Task<List<TableInfo>> GetAllTablesInfos()
    {
        return await _getAllTablesInfosHandler.GetTablesStatusesInfo();
    }

    public async Task<bool> TryConnect()
    {
        return await _tryConnectHandler.TryConnectAsync();
    }

    public async Task<List<MlObjectModel>> DownloadTable(string tableName)
    {
        return await _downloadTableHandler.DownloadTable(tableName);
    }
}