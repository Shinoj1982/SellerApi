using Seller.API.Models;

namespace Seller.API.Queue
{
    public interface IMessageQueueClient
    {
        public Task SendAsync(Product product);
    }
}
