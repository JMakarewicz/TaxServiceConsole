using System;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public class OrderTaxResponse
    {
        [JsonProperty("tax")]
        public Tax OrderTax { get; set; } = null;
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Tax
    {
        [JsonProperty("order_total_amount")]
        public float OrderTotalAmount { get; set; } = default(float);
        [JsonProperty("shipping")]
        public float ShippingFee { get; set; } = default(float);
        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; } = default(float);
        [JsonProperty("amount_to_collect")]
        public float AmountToCollect { get; set; } = default(float);
        [JsonProperty("rate")]
        public float TaxRate { get; set; } = default(float);
        [JsonProperty("has_nexus")]
        public bool HasNexus { get; set; } = default(bool);
        [JsonProperty("freight_taxable")]
        public bool IsFreightTaxable { get; set; } = default(bool);
        [JsonProperty("tax_source")]
        public string TaxSource { get; set; } = String.Empty;
        [JsonProperty("exemption_type")]
        public ExemptionType Exemption { get; set; } = ExemptionType.other;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Order Amount:\t{0}\r\n", OrderTotalAmount.ToString("$0.00"));
            sb.AppendFormat("Taxable Amount:\t{0}\r\n", TaxableAmount.ToString("$0.00"));
            sb.AppendFormat("Tax Rate:\t{0}\r\n", TaxRate.ToString("0.00000 %"));
            sb.AppendFormat("Tax Amount:\t{0}\r\n", AmountToCollect.ToString("$0.00"));
            sb.AppendLine();

            if(!String.IsNullOrWhiteSpace(TaxSource))
            {
                sb.Append("Tax Source:  ");
                sb.Append(TaxSource);
                sb.AppendLine();
            }

            sb.Append("Freight Taxable:  ");
            sb.Append(IsFreightTaxable ? "Yes" : "No");
            sb.AppendLine();
            sb.Append("Exemption Status:  ");
            sb.Append(Exemption.ToString().Replace('_', ' '));
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
