using Seller.API.Models;

namespace Seller.API.Data
{
    public interface ISellerRepository
    {
        public Task<List<Product?>> GetProductsAsync();

        public Task<Product?> GetProductAsync(string id);

        public Task CreateProductAsync(Product newProduct);

        public Task RemoveProductAsync(string id);
    }
}
