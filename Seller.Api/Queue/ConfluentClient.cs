using Confluent.Kafka;
using Seller.API.Models;

namespace Seller.API.Queue
{
    public class ConfluentClient
    {
        private ClientConfig clientConfig;

        public ConfluentClient()
        {
            this.clientConfig = new ClientConfig();
            this.clientConfig.BootstrapServers = "pkc-41mxj.uksouth.azure.confluent.cloud:9092";
            this.clientConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            this.clientConfig.SaslMechanism = SaslMechanism.Plain;
            this.clientConfig.SaslUsername = "UJSL33IC45D2JY3A";
            this.clientConfig.SaslPassword = "nmZh4i8bCLaAi5ouAlZdBu129bUGTSNLAvomJ9P0M9SFkygaBVWu3stD7Fn8WjHr";
            this.clientConfig.SocketTimeoutMs = 45000;
        }

        public async Task SendAsync(Product product)
        {
            using (var producer = new ProducerBuilder<string, string>(this.clientConfig).Build())
            {
                const string topic = "EAuctionProduct";
                await producer.ProduceAsync(
                    topic, 
                    new Message<string, string> { Key = product.Id, Value = product.ProductName });

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
