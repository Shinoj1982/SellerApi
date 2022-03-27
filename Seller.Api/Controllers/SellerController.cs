using Microsoft.AspNetCore.Mvc;
using Seller.API.Data;
using Seller.API.Models;
using Seller.API.Queue;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Seller.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private ISellerRepository sellerRepository;
        private IBidRepository bidRepository;
        private IMessageQueueClient messageQueueClient;

        public SellerController(
            ISellerRepository sellerRepository,
            IBidRepository bidRepository,
            IMessageQueueClient messageQueueClient)
        {
            this.sellerRepository = sellerRepository;
            this.bidRepository = bidRepository;
            this.messageQueueClient = messageQueueClient;
        }

        // GET api/v1/Seller/getproducts>
        [HttpGet(Name = "getproducts")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await this.sellerRepository.GetProductsAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET api/v1/Seller/getproduct>
        [HttpGet()]
        [Route("show-bids/{productId}", Name = "getproduct")]
        public async Task<ActionResult<ProductDetail>> Get(string productId)
        {
            var product = await this.sellerRepository.GetProductAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var bids = await this.bidRepository.GetBidsAsync(productId);
            var productDetail = new ProductDetailBuilder().WithProduct(product).WithBids(bids).Build();
            return Ok(productDetail);
        }

        // POST api/v1/Seller/add-product>
        [HttpPost("add-product")]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await this.sellerRepository.CreateProductAsync(product);
            await this.messageQueueClient.SendAsync(product);
            return CreatedAtRoute("getproduct", new { productId = product.Id }, product);
        }

        // DELETE api/<SellerController>/5
        [Route("[action]/{productId}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(string productId)
        {
            var product = await this.sellerRepository.GetProductAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            if (product.BidEndDate < DateTime.Now)
            {
                return BadRequest("Product cannot be deleted after bid end date.");
            }
            var bids = await this.bidRepository.GetBidsAsync(productId);
            if (bids.Count > 0)
            {
                return BadRequest("Product cannot be deleted if atleast one bid placed.");
            }

            await this.sellerRepository.RemoveProductAsync(productId);
            return Ok();
        }
    }
}
