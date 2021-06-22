using System;

namespace Common
{
    public class Order
    {
        public Address Origin { get; set; } = null;
        public Address Destination { get; set; } = null;
        public float OrderAmount { get; set; } = default(float);
        public float ShippingFee { get; set; } = default(float);
        public string CustomerId { get; set; } = String.Empty;
        public ExemptionType Exemption { get; set; } = ExemptionType.non_exempt;
    }

    public enum ExemptionType
    {
        non_exempt,
        wholesale,
        government,
        marketplace,
        other,
    }
}
