using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Seller.API.Controllers;
using Seller.API.Data;
using Seller.API.Models;
using Seller.API.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seller.Unit.Tests.Controllers
{
    [TestClass]
    public class SellerControllerTest
    {
        private Mock<ISellerRepository> sellerRepository;
        private Mock<IBidRepository> bidRepository;
        private Mock<IMessageQueueClient> messageQueueClient;
        private SellerController underTest;
        private Product product;

        [TestInitialize]
        public void TestSetup()
        {
            this.product = new Product
            {
                Id = "Product1",
                ProductName = "Product 1",
                BidEndDate = System.DateTime.Now.AddDays(1)
            };

            this.sellerRepository= new Mock<ISellerRepository>();
            this.bidRepository= new Mock<IBidRepository>();
            this.messageQueueClient= new Mock<IMessageQueueClient>();
            this.underTest = new SellerController(this.sellerRepository.Object, this.bidRepository.Object, this.messageQueueClient.Object);
        }

        [TestMethod]
        public void Implements_ControllerBase()
        {
            Assert.IsInstanceOfType(this.underTest, typeof(ControllerBase));
        }

        [TestMethod]
        public async Task Get_InvalidProductId_Returns_NotFound()
        {
            var result = await this.underTest.Get("Invalid");

            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Get_ValidId_ReturnsProduct()
        {
            // Arrange
            var productDetail = new ProductDetailBuilder().WithProduct(this.product).Build();
            this.sellerRepository
                .Setup(repo => repo.GetProductAsync(It.IsAny<string>()))
                .ReturnsAsync(this.product);
            this.underTest = new SellerController(this.sellerRepository.Object, this.bidRepository.Object, this.messageQueueClient.Object);

            // Act
            var result = await this.underTest.Get(this.product.Id);
            var actualResult = (OkObjectResult)result.Result;
            // Assert
            Assert.AreEqual(productDetail.ProductName, ((ProductDetail)actualResult.Value).ProductName);
        }

        [TestMethod]
        public async Task Post_Returns_Success()
        {
            var result = await this.underTest.Post(this.product);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
            this.sellerRepository.Verify(repo => repo.CreateProductAsync(It.IsAny<Product>()), Times.Once());
            this.messageQueueClient.Verify(client => client.SendAsync(It.IsAny<Product>()), Times.Once());
        }

        [TestMethod]
        public async Task Delete_Returns_NotFound_InvalidProductId()
        {
            this.bidRepository
                .Setup(rep => rep.GetBidsAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Bid>());
            var result = await this.underTest.Delete("Invalid");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_Returns_Success()
        {
            this.sellerRepository
                .Setup(repo => repo.GetProductAsync(It.IsAny<string>()))
                .ReturnsAsync(this.product);
            this.bidRepository
                .Setup(rep => rep.GetBidsAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Bid>());
            var result = await this.underTest.Delete(this.product.Id);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
