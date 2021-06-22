using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxService;
using TaxCalculator;
using Common;
using System;

namespace UnitTests
{
    [TestClass]
    public class TaxServiceImplementationTests
    {
        private ITaxService _service = null;

        public TaxServiceImplementationTests()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("TestClient");
            _service = TaxService.TaxService.GetTaxService(calculator);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxRatesForLocationNullLocation()
        {
            Rate rate = _service.GetTaxRatesForLocation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxRatesForLocationEmptyLocation()
        {
            Rate rate = _service.GetTaxRatesForLocation(new Location());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxRatesForLocationUninitializedLocation()
        {
            Location location = new Location();
            location.TaxLocation = new Address();
            Rate rate = _service.GetTaxRatesForLocation(location);
        }

        [TestMethod]
        public void GetTaxRatesForLocationGoodLocation()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocation());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxRatesForLocationNoZip()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoZip());
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoCountry()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoCountry());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoState()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoState());
            Assert.IsFalse(string.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoCity()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoCity());
            Assert.IsFalse(String.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationNoStreet()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationNoStreet());
            Assert.IsFalse(String.IsNullOrWhiteSpace(rate.PostalCode));
        }

        [TestMethod]
        public void GetTaxRatesForLocationInvalidZip()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationInvalidZip());
            Assert.IsNull(rate.City);
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxRatesForLocationBadZip()
        {
            Rate rate = _service.GetTaxRatesForLocation(LocationFactory.GenerateLocationBadZip());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderNullOrder()
        {
            Tax tax = _service.CalculateTaxesForOrder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderEmptyOrder()
        {
            Tax tax = _service.CalculateTaxesForOrder(new Order());
        }

        [TestMethod]
        public void GetTaxesForOrder()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrder());
            Assert.IsTrue(tax.AmountToCollect > 0.00f);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderNoFromAddress()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderNoFromAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTaxesForOrderNoToAddress()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderNoToAddress());
        }

        [TestMethod]
        public void GetTaxesForOrderExempt()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderExempt());
            Assert.AreEqual(tax.AmountToCollect, 0.00f);
        }

        [TestMethod]
        public void GetTaxesForOrderNoAmount()
        {
            Order order = OrderFactory.GenerateOrderNoAmount();
            Tax tax = _service.CalculateTaxesForOrder(order);
            Assert.AreEqual(Math.Round(tax.AmountToCollect, 2),
                 Math.Round(order.ShippingFee * tax.TaxRate, 2));
        }

        [TestMethod]
        public void GetTaxesForOrderNoShipping()
        {
            Order order = OrderFactory.GenerateOrderNoShipping();
            Tax tax = _service.CalculateTaxesForOrder(order);
            Assert.AreEqual(Math.Round(tax.AmountToCollect, 2),
                Math.Round(order.OrderAmount * tax.TaxRate, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderCountryNoToZip()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderCountryNoToZip());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderCountryNoToState()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderCountryNoToState());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderInvalidFromAddress()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderInvalidFromAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderInvalidToAddress()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderInvalidToAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderBadFromAddress()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderBadFromAddress());
        }

        [TestMethod]
        [ExpectedException(typeof(Newtonsoft.Json.JsonReaderException))]
        public void GetTaxesForOrderBadToAddress()
        {
            Tax tax = _service.CalculateTaxesForOrder(OrderFactory.GenerateOrderBadToAddress());
        }
    }
}
