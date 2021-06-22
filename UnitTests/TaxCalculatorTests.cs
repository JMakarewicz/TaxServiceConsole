using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator;
using Common;
using System;

namespace UnitTests
{
    [TestClass]
    public class TaxCalculatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeWithNull()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeWithEmptyString()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator(String.Empty);
        }

        [TestMethod]
        public void InitializeWithValidClient()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("TestClient");
            Assert.IsNotNull(calculator);
        }

        [TestMethod]
        public void InitializeWithInvalidClient()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("NonExistent");
            Assert.IsNotNull(calculator);
        }

        [TestMethod]
        public void InitializeWithSpecialChars()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("!@#$%^&*()");
            Assert.IsNotNull(calculator);
        }

        [TestMethod]
        public void InitializeWithClientWithBadCalculatorSetting()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("BadClient");
            Assert.IsNull(calculator);
        }
    }
}
