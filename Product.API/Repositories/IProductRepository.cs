using API.Model;

namespace API.Repositories
{
	public interface IProductRepository
	{
		Product Get();
		IEnumerable<Product> GetAll();
		Product Create(Product create);
		Product Update(Product update);
		void Delete(Guid productId);
	}
}

