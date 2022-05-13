namespace MlServer.Database.Repository
{
    public class MlServerRepository
    {
        private readonly MlServerDbContext _dbContext;

        public MlServerRepository(MlServerDbContext context)
        {
            _dbContext = context;
        }

        // public async Task AddProductCategoriesListAsync(List<ProductCategoryDb> productCategories)
        // {
        //     await _dbContext.ProductCategories.AddRangeAsync(productCategories);
        //     await _dbContext.SaveChangesAsync();
        // }
        //
        // public async Task<List<ProductCategoryDb>> GetAllAsync()
        // {
        //     var result =
        //         await _dbContext.ProductCategories.Select(x => x).ToListAsync();
        //
        //     await _dbContext.SaveChangesAsync();
        //     return result;
        // }
    }
}