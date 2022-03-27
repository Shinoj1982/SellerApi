using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization.Attributes;
using Seller.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Seller.Unit.Tests.Models
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void Assert_Attributes_Id()
        {
            Utility.Assert_Property<Product, BsonIdAttribute>("Id");
            Utility.Assert_Property<Product, BsonRepresentationAttribute>("Id");
        }

        [TestMethod]
        public void Assert_Attributes_ProductName()
        {
            this.Assert_RequiredAttribute("ProductName");
            Utility.Assert_Property<Product, StringLengthAttribute>("ProductName");
        }

        [TestMethod]
        public void Assert_Attributes_ShortDescription()
        {
            this.Assert_RequiredAttribute("ShortDescription");
        }

        [TestMethod]
        public void Assert_Attributes_DetailedDescription()
        {
            this.Assert_RequiredAttribute("DetailedDescription");
        }

        [TestMethod]
        public void Assert_Attributes_Category()
        {
            this.Assert_RequiredAttribute("Category");
        }

        private void Assert_RequiredAttribute(string propertyName)
        {
            Utility.Assert_Property<Product, RequiredAttribute>(propertyName);
        }
    }
}
