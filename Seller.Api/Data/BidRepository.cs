using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Seller.API.Models;

namespace Seller.API.Data
{
    public class BidRepository : IBidRepository
    {
        private IMongoCollection<Bid> Bids { get; }

        public BidRepository(IOptions<SellerDatabaseSettings> sellerDatabaseSettings)
        {
            var mongoClient = new MongoClient(sellerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
            sellerDatabaseSettings.Value.DatabaseName);

            this.Bids = mongoDatabase.GetCollection<Bid>(
                sellerDatabaseSettings.Value.BidCollection);
        }

        public async Task<List<Bid>> GetBidsAsync(string productId) =>
             await Bids.Find<Bid>(p => p.ProductId == productId).ToListAsync();

    }
}
