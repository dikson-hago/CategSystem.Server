using Grpc.Core;
using MlServer.Application.Handlers;
using MlServer.Hosting.Mapper;
using MlServer.Proto;

using Proto = MlServer.Hosting;

namespace MlServer.Hosting.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ApplicationHandlers _handlers;

    public GreeterService(ApplicationHandlers handlers)
    {
        _handlers = handlers; 
    }
    
    public override async Task<ErrorsResultResponse> AddObjects(AddObjectsRequest request, ServerCallContext context)
    {
        var result = await _handlers.AddObjects(request.MapToObjectsInfos(), 
            request.TableName);
        
        return result.MapToErrorsResultResponse();
    }

    public override async Task<ErrorsResultResponse> AddTable(AddTableRequest request, ServerCallContext context)
    {
        var result = await _handlers.AddTable(request.MapToTableInfo());
        return result.MapToErrorsResultResponse();
    }

    public override async Task<GetObjectsResponse> PredictCategory(PredictRequest request, ServerCallContext context)
    {
        var result = 
            await _handlers.PredictCategory(request.MapToObjectsCategoryList(), request.TableName);

        return result.MapToObjectsResponse();
    }

    public override Task<GetTablesStatusesResponse> GetTablesStatuses(Empty request, ServerCallContext context)
    {
        var result = 
            _handlers.GetTablesStatuses();

        return Task.FromResult(result.MapToGetTablesStatusesResponse());
    }

    public override async Task<GetAllTablesResponse> GetAllTablesInfos(Empty request, ServerCallContext context)
    {
        var result = await _handlers.GetAllTablesInfos();

        return result.MapToGetAllTablesResponse();
    }

    public override async Task<Empty> TryConnect(Empty request, ServerCallContext context)
    {
        await Task.Yield();
        return new Empty();
    }

    public override async Task<GetObjectsResponse> DownloadTable(DownloadTableRequest request, ServerCallContext context)
    {
        var result = await _handlers.DownloadTable(request.TableName);

        return result.MapToObjectsResponse();
    }
}