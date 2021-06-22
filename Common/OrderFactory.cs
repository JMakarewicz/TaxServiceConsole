using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class OrderFactory
    {
        public static Order GenerateOrder()
        {
            Order order = new Order();
            Address from = new Address();
            Address to = new Address();

            from.Country = "US";
            from.City = "Clifton";
            from.StateOrProvince = "NJ";
            from.Street = "68 Sipp Avenue";
            from.PostalCode = "07013";
            order.Origin = from;
            to.Country = "US";
            to.City = "Parsippany";
            to.StateOrProvince = "NJ";
            to.PostalCode = "07054";
            to.Street = "3379 US Highway 46";
            order.Destination = to;
            order.OrderAmount = 80.00f;
            order.ShippingFee = 12.95f;
            order.CustomerId = "123456";
            order.Exemption = ExemptionType.marketplace;

            return order;
        }

        public static Order GenerateOrderNoFromAddress()
        {
            Order order = GenerateOrder();
            order.Origin = null;

            return order;
        }

        public static Order GenerateOrderNoToAddress()
        {
            Order order = GenerateOrder();
            order.Destination = null;

            return order;
        }

        public static Order GenerateOrderNoAmount()
        {
            Order order = GenerateOrder();
            order.OrderAmount = 0.00f;

            return order;
        }

        public static Order GenerateOrderNoShipping()
        {
            Order order = GenerateOrder();
            order.ShippingFee = 0.00f;

            return order;
        }

        public static Order GenerateOrderCountryNoToZip()
        {
            Order order = GenerateOrder();
            order.Destination.PostalCode = String.Empty;

            return order;
        }

        public static Order GenerateOrderCountryNoToState()
        {
            Order order = GenerateOrder();
            order.Destination.StateOrProvince = String.Empty;

            return order;
        }

        public static Order GenerateOrderExempt()
        {
            Order order = GenerateOrder();
            order.Exemption = ExemptionType.government;

            return order;
        }

        public static Order GenerateOrderInvalidFromAddress()
        {
            Order order = GenerateOrder();
            order.Origin.PostalCode = "00000-0000";

            return order;
        }

        public static Order GenerateOrderInvalidToAddress()
        {
            Order order = GenerateOrder();
            order.Destination.PostalCode = "00000-0000";

            return order;
        }

        public static Order GenerateOrderBadFromAddress()
        {
            Order order = GenerateOrder();
            order.Origin.PostalCode = "!@#$%^&*()";

            return order;
        }

        public static Order GenerateOrderBadToAddress()
        {
            Order order = GenerateOrder();
            order.Destination.PostalCode = "!@#$%^&*()";

            return order;
        }
    }
}
