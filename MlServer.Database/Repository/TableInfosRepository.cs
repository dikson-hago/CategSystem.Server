using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MlServer.Database.Models;

using ContractDb = MlServer.Contracts.Models.Db;

namespace MlServer.Database.Repository;

public class TableInfosRepository
{
    private MlServerDbContext _dbContext;

    public TableInfosRepository(MlServerDbContext context)
    {
        _dbContext = context;
    }

    public async Task<bool> EnsureTableExist(string tableName)
    {
        var result = await _dbContext
            .TablesInfosTable.Where(x => x.TableName.Equals(tableName))
            .ToListAsync();

        return result.Count > 0;
    }

    public async Task AddTable(ContractDb.TableInfo tableInfo)
    {
        await _dbContext.TablesInfosTable.AddAsync(new TableDbInfo()
        {
            TableName = tableInfo.TableName,
            ColumnsAmount = tableInfo.ColumnsAmount,
            CategoryColumnName = tableInfo.CategoryColumnName,
            ColumnNames = tableInfo.ColumnNamesInJson
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Dictionary<string, long>> GetAllTableNamesAndColumnsAmount()
    {
         return await
            _dbContext.TablesInfosTable
                .ToDictionaryAsync(x => x.TableName, x => x.ColumnsAmount);
    }

    public async Task<List<ContractDb.TableInfo>> GetAllTablesInfos()
    {
       return await _dbContext.TablesInfosTable.Select(item => new ContractDb.TableInfo()
        {
            ColumnNamesInJson = item.ColumnNames,
            CategoryColumnName = item.CategoryColumnName,
            TableName = item.TableName
        }).ToListAsync();
    }

    public async Task<ContractDb.TableInfo> GetTableInfo(string tableName)
    {
        var result = await _dbContext.TablesInfosTable
            .Select(table => new ContractDb.TableInfo()
            {
                TableName = table.TableName,
                CategoryColumnName = table.CategoryColumnName,
                ColumnNamesInJson = table.ColumnNames,
                ColumnsAmount = table.ColumnsAmount
            }).FirstOrDefaultAsync(table => table.TableName.Equals(tableName));
        
        return result;
    }

    public async Task<List<string>> GetTableColumnNames(string tableName)
    {
        var result = await _dbContext.TablesInfosTable
            .FirstOrDefaultAsync(table => table.TableName.Equals(tableName))!;
        
        if (result is null)
        {
            throw new Exception("Current table doesn't exist");
        }
        
        var columnNames = JsonSerializer.Deserialize<List<string>>(result.ColumnNames);

        if (columnNames is null)
        {
            throw new Exception("Current table doesn't contains columnNames ");
        }

        var headers = new List<string>();
        
        headers.Add(result.CategoryColumnName);
        
        headers.AddRange(columnNames);

        return headers;
    }
}