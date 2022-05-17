using MlServer.Application.Errors;
using MlServer.Contracts.Errors;
using MlServer.Database.Repository;
using MlServer.Orchestrator.Learners.Distriburtors;
using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Application.Handlers;

public class AddTableHandler 
{
    private readonly TableInfosRepository _repository;
    private readonly AddNewLearnersDistributor _distributor;
    private ErrorsInCurrentSession _errors;
    private readonly AddedTableValidator _validator;
    
    public AddTableHandler(
        TableInfosRepository repository,
        AddNewLearnersDistributor distributor,
        ErrorsInCurrentSession errors,
        AddedTableValidator validator)
    {
        _repository = repository;
        _distributor = distributor;
        _errors = errors;
        _validator = validator;
    }

    public async Task<ErrorsResponse> Handle(ContractDb.TableInfo tableInfo)
    {
        try
        {
            // validate table
            await _validator.Validate(tableInfo);

            // add learners
            _distributor.AddNewLearner(tableInfo.TableName);

            // add table
            await _repository.AddTable(tableInfo);
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