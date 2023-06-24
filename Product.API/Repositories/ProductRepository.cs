using API.Model;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        public Task<Product> CreateAsync(Product create)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product update)
        {
            throw new NotImplementedException();
        }
    }
}

