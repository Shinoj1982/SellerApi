namespace Seller.API.Models
{
    public class ProductDetail
    {
        public string ProductName { get; set; }

        public string ShortDescription { get; set; }

        public string DetailedDescription { get; set; }

        public ProductCategory Category { get; set; }

        public decimal StartingPrice { get; set; }

        public DateTime BidEndDate { get; set; }

        public List<Bid> Bids { get; set; }
    }
}
