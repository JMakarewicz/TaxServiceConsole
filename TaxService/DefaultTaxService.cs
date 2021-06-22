using Common;

namespace TaxService
{
    internal class DefaultTaxService:ITaxService
    {
        protected ITaxCalculator _calculator;
        
        public DefaultTaxService(ITaxCalculator calculator)
        {
            _calculator = calculator;    
        }

        //As per spec:  The Tax Service will also have these methods and simply call the Tax Calculator.
        public Tax CalculateTaxesForOrder(Order order)
        {
            return _calculator.CalculateTaxesForOrder(order);
        }

        public Rate GetTaxRatesForLocation(Location location)
        {
            return _calculator.GetTaxRatesForLocation(location);
        }
    }
}
