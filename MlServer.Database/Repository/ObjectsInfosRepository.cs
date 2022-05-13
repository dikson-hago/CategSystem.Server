using Microsoft.EntityFrameworkCore;
using MlServer.Database.Mapper;

using ContractDb = MlServer.Contracts.Models.Db;
using ContractMl = MlServer.Contracts.Models.ML;

namespace MlServer.Database.Repository;

public class ObjectsInfosRepository
{
    private readonly MlServerDbContext _dbContext;

    public ObjectsInfosRepository(MlServerDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task AddObjectsListAsync(List<ContractDb.ObjectInfo> objectInfos)
    {
        await _dbContext.ObjectsTable.AddRangeAsync(objectInfos.MapToObjectDbInfo());
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ContractMl.ObjectCategory>> GetObjectCategoryList(string tableName)
    {
        return await _dbContext.ObjectsTable.Where(x => x.TableName == tableName)
            .Select(x => x.MapToContractMlObjectCategory()).ToListAsync();
    }

    public async Task<List<ContractDb.ObjectInfo>> GetTableByName(string tableName)
    {
        var table =
            _dbContext.ObjectsTable.Where(item => item.TableName.Equals(tableName)).ToList();

        return table.MapToObjectInfo();
    }
}