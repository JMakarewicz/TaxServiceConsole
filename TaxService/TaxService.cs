using Common;
using System;

namespace TaxService
{
    public static class TaxService
    {
        public static ITaxService GetTaxService(ITaxCalculator calculator)
        {
            if(Object.Equals(calculator, null))
            {
                throw new ArgumentNullException("calculator");
            }

            return new DefaultTaxService(calculator);
        }
    }
}
