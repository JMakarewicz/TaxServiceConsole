namespace Common
{
    public interface ITaxCalculator
    {
        Rate GetTaxRatesForLocation(Location location);
        Tax CalculateTaxesForOrder(Order order);
    }
}
