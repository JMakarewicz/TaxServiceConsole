using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator;
using TaxService;
using Common;
using System;
using System.IO;

//Note:  These tests require that a TaxCalculators directory exist in the bin directory with the
//TaxJarCalculator.dll and dependencies in it, and the config.json file has to be in the test root
//directory.  Under "true" circumstances this would be handled differently, such as a central build
//location.
namespace UnitTests
{
    [TestClass]
    
    public class TaxServiceTests
    {
        public readonly string DEBUG_PATH = 
            @"..\..\..\..\TaxServiceConsole\bin\Debug\netcoreapp3.1\";
        

        [TestMethod]
        public void InstantiateWithValidCalculator()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("TestClient");
            ITaxService service = TaxService.TaxService.GetTaxService(calculator);
            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstantiateWithNullCalculator()
        {
            ITaxService service = TaxService.TaxService.GetTaxService(null);
            

        }

        [TestMethod]
        public void InstantiateWithInvalidClient()
        {
            ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("NonExistent");
            ITaxService service = TaxService.TaxService.GetTaxService(calculator);
            Assert.IsNotNull(service);
        }
    }
}
