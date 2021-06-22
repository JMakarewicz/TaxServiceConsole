using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator;
using Common;
using System;

namespace UnitTests
{
    [TestClass]
    
    public class TaxJarCalculatorTests
    {
        private ITaxCalculator _calculator = null;

        public TaxJarCalculatorTests()
        {
            _calculator = TaxCalculator.TaxCalculator.GetCalculator("TestClient");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxRatesForLocationNullLocation()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxRatesForLocationEmptyLocation()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(new Location());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxRatesForLocationUninitializedLocation()
        {
            Location location = new Location();
            location.TaxLocation = new Address();
            Rate rate = _calculator.GetTaxRatesForLocation(location);
        }

        [TestMethod]
        public void GetTaxRatesForLocationGoodLocation()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocation());
            Assert.IsFalse(String.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxRatesForLocationNoZip()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoZip());
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoCountry()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoCountry());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoState()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoState());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoCity()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoCity());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoStreet()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoStreet());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationInvalidZip()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationInvalidZip());
            Assert.IsNull(rate.City);
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxRatesForLocationBadZip()
        {
            Rate rate = _calculator.GetTaxRatesForLocation(LocationFactory.GenerateLocationBadZip());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderNullOrder()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderEmptyOrder()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(new Order());
        }

        [TestMethod]
        public void GetTaxesForOrder()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrder());
            Assert.IsTrue(tax.AmountToCollect > 0.00f);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderNoFromAddress()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderNoFromAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderNoToAddress()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderNoToAddress());
        }

        [TestMethod]
        public void GetTaxesForOrderExempt()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderExempt());
            Assert.AreEqual(tax.AmountToCollect, 0.00f);
        }

        [TestMethod]
        public void GetTaxesForOrderNoAmount()
        {
            Order order = OrderFactory.GenerateOrderNoAmount();
            Tax tax = _calculator.CalculateTaxesForOrder(order);
            Assert.AreEqual(Math.Round(tax.AmountToCollect, 2),
                 Math.Round(order.ShippingFee * tax.TaxRate, 2));
        }

        [TestMethod]
        public void GetTaxesForOrderNoShipping()
        {
            Order order = OrderFactory.GenerateOrderNoShipping();
            Tax tax = _calculator.CalculateTaxesForOrder(order);
            Assert.AreEqual(Math.Round(tax.AmountToCollect, 2), 
                Math.Round(order.OrderAmount * tax.TaxRate, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderCountryNoToZip()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderCountryNoToZip());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderCountryNoToState()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderCountryNoToState());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderInvalidFromAddress()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderInvalidFromAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderInvalidToAddress()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderInvalidToAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderBadFromAddress()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderBadFromAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderBadToAddress()
        {
            Tax tax = _calculator.CalculateTaxesForOrder(OrderFactory.GenerateOrderBadToAddress());
        }
    }
}
