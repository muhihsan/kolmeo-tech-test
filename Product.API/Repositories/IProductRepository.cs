using API.Model;

namespace API.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid guid);
    Task<IEnumerable<Product>> GetAllAsync(int pageSize = 10, int pageIndex = 0);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    void DeleteAsync(Guid productId);
    Task<long> Count();
}

