namespace Common
{
    public interface ITaxService
    {
        Rate GetTaxRatesForLocation(Location location);
        Tax CalculateTaxesForOrder(Order order);
    }
}
