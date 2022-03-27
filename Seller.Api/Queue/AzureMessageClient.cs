using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Seller.API.Models;

namespace Seller.API.Queue
{
    public class AzureMessageClient : IMessageQueueClient
    {
        private string connectionString;
        private string queueName;

        public AzureMessageClient(IOptions<AzureServiceBusSettings> serviceBusSettings)
        {
            this.connectionString = serviceBusSettings.Value.ConnectionString;
            this.queueName = serviceBusSettings.Value.QueueName;
        }

        public async Task SendAsync(Product product)
        {
            var client = new ServiceBusClient(this.connectionString);
            var sender = client.CreateSender(this.queueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            
            try
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(product.Id)))
                {
                    // if it is too large for the batch
                    throw new Exception($"Error trying to send {product.Id} to service bus.");
                }

                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
