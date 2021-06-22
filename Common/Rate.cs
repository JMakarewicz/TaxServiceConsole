using System;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public class LocationRateResponse
    {
        [JsonProperty("rate")]
        public Rate LocationRate { get; set; } = null;
    }
    public class Rate
    {
        [JsonProperty("zip")]
        public string PostalCode { get; set; } = String.Empty;
        [JsonProperty("country")]
        public string Country { get; set; } = String.Empty;
        [JsonProperty("country_rate")]
        public float CountryRate { get; set; } = default(float);
        [JsonProperty("state")]
        public string StateOrProvince { get; set; } = String.Empty;
        [JsonProperty("state_rate")]
        public float StateOrProvinceRate { get; set; } = default(float);
        [JsonProperty("county")]
        public string CountyOrRegion { get; set; } = String.Empty;
        [JsonProperty("county_rate")]
        public float CountyOrRegionRate { get; set; } = default(float);
        [JsonProperty("city")]
        public string City { get; set; } = String.Empty;
        [JsonProperty("city_rate")]
        public float CityRate { get; set; } = default(float);
        [JsonProperty("combined_district_rate")]
        public float CombinedDistrictRate { get; set; } = default(float);
        [JsonProperty("combined_rate")]
        public float CombinedRate { get; set; } = default(float);
        [JsonProperty("freight_taxable")]
        public bool IsFreightTaxable { get; set; } = default(bool);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Rate Location Zip:  ");
            sb.Append(PostalCode);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("Country ({0}):  {1}\r\n", Country, CountryRate.ToString("0.00000"));
            sb.AppendFormat("State ({0}):  {1}\r\n", StateOrProvince, StateOrProvinceRate.ToString("0.00000"));
            sb.AppendFormat("County ({0}):  {1}\r\n", CountyOrRegion, CountyOrRegionRate.ToString("0.00000"));
            sb.AppendFormat("City ({0}):  {1}\r\n", City, CityRate.ToString("0.00000"));
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("District Rate:  ");
            sb.Append(CombinedDistrictRate.ToString("0.00000"));
            sb.AppendLine();
            sb.Append("Combined Rate:  ");
            sb.Append(CombinedRate.ToString("0.00000"));
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Freight Taxable:  ");
            sb.Append(IsFreightTaxable ? "Yes" : "No");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
