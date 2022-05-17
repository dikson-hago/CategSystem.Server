using MlServer.Application.Errors;
using MlServer.Contracts.Errors;
using MlServer.Database.Repository;
using MlServer.Orchestrator.Learners;
using ContractDbModel = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class AddObjectsHandler
{
    private readonly ObjectsInfosRepository _objectsInfosRepository;
    private readonly RetrainTablesInfo _retrainTablesInfo;
    private ErrorsInCurrentSession _errors;
    private readonly AddedObjectsValidator _validator;

    public AddObjectsHandler(
        ObjectsInfosRepository objectsInfosRepository,
        ErrorsInCurrentSession errors,
        AddedObjectsValidator validator)
    {
        _objectsInfosRepository = objectsInfosRepository;
        _retrainTablesInfo = RetrainTablesInfo.GetInstance();
        _errors = errors;
        _validator = validator;
    }

    public async Task<ErrorsResponse> Handle(List<ContractDbModel.ObjectInfo> objectInfos,
        string tableName)
    {
        try
        {
            // validate data
            await _validator.Validate(objectInfos, tableName);
            
            // try add to table
            await _objectsInfosRepository.AddObjectsListAsync(objectInfos);

            // get unique new table names
            var uniqueTableNames = objectInfos.Select(item => item.TableName)
                .Distinct();

            _retrainTablesInfo.Add(tableName);
        }
        catch (Exception e)
        {   
            _errors.AddError(e.Message);
        }

        return new ErrorsResponse()
        {
            ErrorsAmount = _errors.GetErrorsAmount(),
            Errors = _errors.GetErrors()
        };
    }

}