using API.Model;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;

namespace API.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public ProductRepository(AppDbContext dbContext)
		{
		}

        public Product Create(Product create)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Product Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product Update(Product update)
        {
            throw new NotImplementedException();
        }
    }
}

