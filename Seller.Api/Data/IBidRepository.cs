using Seller.API.Models;

namespace Seller.API.Data
{
    public interface IBidRepository
    {
        public Task<List<Bid>> GetBidsAsync(string productId);
    }
}
