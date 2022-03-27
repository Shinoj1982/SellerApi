using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seller.API.Models;

namespace Seller.Unit.Tests.Models
{
    [TestClass]
    public class ProductCategoryTest
    {
        [TestMethod]
        public void Assert_ProductCategory_Painting()
        {
            Assert.AreEqual((int)ProductCategory.Painting, 0);
            Assert.AreEqual((int)ProductCategory.Ornament, 2);
            Assert.AreEqual((int)ProductCategory.Sculptor, 1);
        }
    }
}
