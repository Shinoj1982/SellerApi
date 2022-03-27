namespace Seller.API.Models
{
    public class SellerDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProductCollection { get; set; } = null!;

        public string BidCollection { get; set; } = null!;
    }
}
