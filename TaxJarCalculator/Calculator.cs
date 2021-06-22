using System;
using Common;
using System.Collections.Generic;
using RestfulClient;

namespace TaxJarCalculator
{
    public class Calculator : ITaxCalculator
    {
        //In most cases this information would be more dynamic, such as in a config file.
        //But API keys should not be stored in clear text, and this is at least semi secure.
        protected readonly string TOKEN = "5da2f821eee4035db4771edab942a4cc";
        protected readonly string LOCATION_TAX_RATE_URL = "https://api.taxjar.com/v2/rates/";
        protected readonly string ORDER_TAXES_URL = "https://api.taxjar.com/v2/taxes";

        //See https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/
        //for details.
        protected Client client = null;

        public Calculator()
        {
            client = new Client(TOKEN);
        }

        public Tax CalculateTaxesForOrder(Order order)
        {
            List<KeyValuePair<string, string>> parameters = GetParamsFromOrder(order);
            string responseString = client.SendPostRequest(ORDER_TAXES_URL, parameters);
            OrderTaxResponse taxResponse = JsonDeserializer.Deserialize<OrderTaxResponse>(responseString);
            return taxResponse.OrderTax;
        }

        public Rate GetTaxRatesForLocation(Location location)
        {
            List<KeyValuePair<string, string>> parameters = GetParamsFromLocation(location);
            string modifiedUrl = String.Concat(LOCATION_TAX_RATE_URL, location.TaxLocation.PostalCode.Trim());
            string responseString = client.SendGetRequest(modifiedUrl, parameters);
            LocationRateResponse locationRate = JsonDeserializer.Deserialize<LocationRateResponse>(responseString);
            return locationRate.LocationRate;
            throw new NotImplementedException();
        }

        protected List<KeyValuePair<string, string>> GetParamsFromOrder(Order order)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

            if(!String.IsNullOrWhiteSpace(order.Origin.Country))
            {
                parameters.Add(new KeyValuePair<string, string>("from_country", order.Origin.Country));
            }

            if (!String.IsNullOrWhiteSpace(order.Origin.PostalCode))
            {
                parameters.Add(new KeyValuePair<string, string>("from_zip", order.Origin.PostalCode));
            }

            if (!String.IsNullOrWhiteSpace(order.Origin.StateOrProvince))
            {
                parameters.Add(new KeyValuePair<string, string>("from_state", order.Origin.StateOrProvince));
            }

            if (!String.IsNullOrWhiteSpace(order.Origin.City))
            {
                parameters.Add(new KeyValuePair<string, string>("from_city", order.Origin.City));
            }

            if (!String.IsNullOrWhiteSpace(order.Origin.Street))
            {
                parameters.Add(new KeyValuePair<string, string>("from_street", order.Origin.Street));
            }

            if (!String.IsNullOrWhiteSpace(order.Destination.Country))
            {
                parameters.Add(new KeyValuePair<string, string>("to_country", order.Destination.Country));
            }

            if (!String.IsNullOrWhiteSpace(order.Destination.PostalCode))
            {
                parameters.Add(new KeyValuePair<string, string>("to_zip", order.Destination.PostalCode));
            }

            if (!String.IsNullOrWhiteSpace(order.Destination.StateOrProvince))
            {
                parameters.Add(new KeyValuePair<string, string>("to_state", order.Destination.StateOrProvince));
            }

            if (!String.IsNullOrWhiteSpace(order.Destination.City))
            {
                parameters.Add(new KeyValuePair<string, string>("to_city", order.Destination.City));
            }

            if (!String.IsNullOrWhiteSpace(order.Destination.Street))
            {
                parameters.Add(new KeyValuePair<string, string>("to_street", order.Destination.Street));
            }

            parameters.Add(new KeyValuePair<string, string>("amount", order.OrderAmount.ToString("0.00")));
            parameters.Add(new KeyValuePair<string, string>("shipping", order.ShippingFee.ToString("0.00")));

            if (!String.IsNullOrWhiteSpace(order.CustomerId))
            {
                parameters.Add(new KeyValuePair<string, string>("customer_id", order.CustomerId));
            }

            parameters.Add(new KeyValuePair<string, string>("exemption_type", order.Exemption.ToString()));

            return parameters;
        }

        protected List<KeyValuePair<string, string>> GetParamsFromLocation(Location location)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

            if(!String.IsNullOrWhiteSpace(location.TaxLocation.Country))
            {
                parameters.Add(new KeyValuePair<string, string>("country", location.TaxLocation.Country));
            }

            if (!String.IsNullOrWhiteSpace(location.TaxLocation.StateOrProvince))
            {
                parameters.Add(new KeyValuePair<string, string>("state", location.TaxLocation.StateOrProvince));
            }

            if (!String.IsNullOrWhiteSpace(location.TaxLocation.City))
            {
                parameters.Add(new KeyValuePair<string, string>("city", location.TaxLocation.City));
            }

            if (!String.IsNullOrWhiteSpace(location.TaxLocation.Street))
            {
                parameters.Add(new KeyValuePair<string, string>("street", location.TaxLocation.Street));
            }

            return parameters;
        }
    }
}
