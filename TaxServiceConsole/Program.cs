using System;
using Common;

namespace TaxServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ITaxService service = null;
            //As per spec:  Eventually we would have several Tax Calculators and the Tax Service
            //would need to decide which to use based on the Customer that is consuming the Tax Service. 
            //The methodology used is flexible enough to allow multiple clients to "reuse" the same calculator
            //and also to allow for a "default" calculator if an entry does not yet exist for that client.
            //Using a json settings file for expediency, but any sort of config scheme could be used.

            try
            {
                ITaxCalculator calculator = TaxCalculator.TaxCalculator.GetCalculator("TestClient");

                if (!Object.Equals(calculator, null))
                {
                    //As per spec:  Your code test is to simply create a Tax Service that can take a Tax Calculator
                    //in the class initialization and return the total tax that needs to be collected.
                    service = TaxService.TaxService.GetTaxService(calculator);

                    if (!Object.Equals(service, null))
                    {
                        Console.WriteLine("Service loaded.");
                    }
                    else
                    {
                        Console.WriteLine("Service load failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Calculator load failed.");
                }

                //Using the TaxJar business objects for generic responses partially breaks encapsulation by forcing
                //all calculators to follow the same model.  In a full-fledged application I would be recommending a
                //generic business object format for these responses, and each calculator would have an added step to 
                //convert specific response objects into the generic format.  But, since no such requirement is listed
                //and no other calculator types are included that step is skipped here.
                Console.WriteLine("Getting Tax Rate:");
                Rate taxRate = service.GetTaxRatesForLocation(LocationFactory.GenerateLocation());
                Console.WriteLine(taxRate.ToString());
                Console.WriteLine();
                //During testing it appeared that the Tax for Order URL does not function correctly when the from
                //and to addresses are not in the same state.  The response is returned, but all tax and order amounts
                //return as zeroes and the tax source is null.  When both addresses are in the same state the response
                //contains values in all fields.  Under normal circumstances this is something that would
                //be thoroughly tested in isolation and followed up on with support channels at TaxJar.
                Console.WriteLine("Getting Taxes For Order:");
                Tax tax = service.CalculateTaxesForOrder(OrderFactory.GenerateOrder());
                Console.WriteLine(tax.ToString());
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Please press any key to exit");
            Console.Read();
        }
    }
}
