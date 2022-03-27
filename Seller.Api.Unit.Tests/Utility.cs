using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Seller.Unit.Tests
{
    internal static class Utility
    {
        public static void Assert_Property<TClass, TAttribute>(string propertyName) 
            where TClass : class
            where TAttribute : System.Attribute
        {
            var t = typeof(TClass);
            var pi = t.GetProperty(propertyName);
            var hasAttribute = Attribute.IsDefined(pi, typeof(TAttribute));
            Assert.IsTrue(hasAttribute);
        }
    }
}
