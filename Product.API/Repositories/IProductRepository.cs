using API.Model;

namespace API.Repositories
{
	public interface IProductRepository
	{
		Task<Product> GetByIdAsync(Guid guid);
		Task<IEnumerable<Product>> GetAllAsync();
		Task<Product> CreateAsync(Product create);
		Task<Product> UpdateAsync(Product update);
		void DeleteAsync(Guid productId);
	}
}

