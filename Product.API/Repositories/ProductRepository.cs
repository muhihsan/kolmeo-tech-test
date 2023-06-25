using API.Infrastructure.Configurations;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ProductRepository : IProductRepository
{
    private AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public void DeleteAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetByIdAsync(Guid guid)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == guid);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int pageSize = 10, int pageIndex = 0)
    {
        return await _dbContext.Products
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<long> Count()
    {
        return await _dbContext.Products.LongCountAsync();
    }
}

