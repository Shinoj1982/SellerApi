using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Seller.API.Models;

namespace Seller.API.Data
{
    public class SellerRepository : ISellerRepository
    {
        private IMongoCollection<Product> Products { get; }

        public SellerRepository(IOptions<SellerDatabaseSettings> sellerDatabaseSettings)
        {
            var mongoClient = new MongoClient(sellerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
            sellerDatabaseSettings.Value.DatabaseName);

            this.Products = mongoDatabase.GetCollection<Product>(
                sellerDatabaseSettings.Value.ProductCollection);
        }

        public async Task<List<Product>> GetProductsAsync() =>
            await Products.Find<Product>(_ => true).ToListAsync();

        public async Task<Product?> GetProductAsync(string id) =>
             await Products.Find<Product>(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateProductAsync(Product newProduct) =>
            await Products.InsertOneAsync(newProduct);

        public async Task RemoveProductAsync(string id) =>
            await Products.DeleteOneAsync(x => x.Id == id);
    }
}
