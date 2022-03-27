namespace Seller.API.Models
{
    public class ProductDetailBuilder
    {
        private Product product;
        private List<Bid> bids;

        public ProductDetailBuilder WithProduct(Product product)
        {
            this.product = product;
            return this;
        }

        public ProductDetailBuilder WithBids(List<Bid> bids)
        {
            this.bids = bids;
            return this;
        }

        public ProductDetail Build()
        {
            return new ProductDetail
            {
                ProductName = this.product.ProductName,
                ShortDescription = this.product.ShortDescription,
                DetailedDescription = this.product.DetailedDescription,
                Category = this.product.Category,
                BidEndDate = this.product.BidEndDate,
                StartingPrice = this.product.StartingPrice,
                Bids = this.bids
            };
        }
    }
}
