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
        public readonly string RELEASE_PATH = 
            @"..\..\..\..\TaxServiceConsole\bin\Release\netcoreapp3.1\";
        public readonly string TAX_CALCULATOR_FOLDER = "TaxCalculators";
        public readonly string SETTINGS_FILE = "config.json";

        public TaxServiceTests()
        {
#if DEBUG
            File.Copy(String.Concat(DEBUG_PATH, SETTINGS_FILE),
                String.Concat(Environment.CurrentDirectory, @"\", SETTINGS_FILE), true);

            if(!Directory.Exists(TAX_CALCULATOR_FOLDER))
            {
                Directory.CreateDirectory(TAX_CALCULATOR_FOLDER);
            }

            foreach(string fileName in Directory.EnumerateFiles(
                String.Concat(DEBUG_PATH, TAX_CALCULATOR_FOLDER), "*.dll"))
            {
                File.Copy(fileName, 
                    fileName.Replace(DEBUG_PATH, String.Concat(Environment.CurrentDirectory, @"\")), true);
            }
#else
                File.Copy(String.Concat(RELEASE_PATH, SETTINGS_FILE),
                String.Concat(Environment.CurrentDirectory, @"\", SETTINGS_FILE), true);

                if(!Directory.Exists(TAX_CALCULATOR_FOLDER))
                {
                    Directory.CreateDirectory(TAX_CALCULATOR_FOLDER);
                }

                foreach(string fileName in Directory.EnumerateFiles(
                String.Concat(RELEASE_PATH, TAX_CALCULATOR_FOLDER), "*.dll"))
                {
                    File.Copy(fileName, 
                        fileName.Replace(RELEASE_PATH, String.Concat(Environment.CurrentDirectory, @"\")), true);
                }
#endif
        }

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
