using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seller.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Seller.Unit.Tests.Models
{
    [TestClass]
    public class SellerTest
    {
        [TestMethod]
        public void Assert_Attributes_FirstName()
        {
            this.Assert_RequiredAttribute("FirstName");
            Utility.Assert_Property<Seller.API.Models.Seller, StringLengthAttribute>("FirstName");
        }

        [TestMethod]
        public void Assert_Attributes_LastName()
        {
            this.Assert_RequiredAttribute("LastName");
            Utility.Assert_Property<Seller.API.Models.Seller, StringLengthAttribute>("LastName");
        }

        [TestMethod]
        public void Assert_Attributes_Address()
        {
            this.Assert_RequiredAttribute("Address");
        }

        [TestMethod]
        public void Assert_Attributes_City()
        {
            this.Assert_RequiredAttribute("City");
        }

        [TestMethod]
        public void Assert_Attributes_State()
        {
            this.Assert_RequiredAttribute("State");
        }

        private void Assert_RequiredAttribute(string propertyName)
        {
            Utility.Assert_Property<Seller.API.Models.Seller, RequiredAttribute >(propertyName);
        }
    }
}
