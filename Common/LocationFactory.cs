using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class LocationFactory
    {
        public static Location GenerateLocation()
        {
            Location location = new Location();
            Address address = new Address();

            address.City = "Parsippany";
            address.Country = "US";
            address.PostalCode = "07054";
            address.StateOrProvince = "NJ";
            address.Street = "3379 US Highway 46";
            location.TaxLocation = address;

            return location;
        }

        public static Location GenerateLocationNoZip()
        {
            Location location = GenerateLocation();
            location.TaxLocation.PostalCode = String.Empty;
            
            return location;
        }

        public static Location GenerateLocationNoCountry()
        {
            Location location = GenerateLocation();
            location.TaxLocation.Country = String.Empty;

            return location;
        }

        public static Location GenerateLocationNoState()
        {
            Location location = GenerateLocation();
            location.TaxLocation.StateOrProvince = String.Empty;

            return location;
        }

        public static Location GenerateLocationNoCity()
        {
            Location location = GenerateLocation();
            location.TaxLocation.City = String.Empty;

            return location;
        }

        public static Location GenerateLocationNoStreet()
        {
            Location location = GenerateLocation();
            location.TaxLocation.Street = String.Empty;

            return location;
        }

        public static Location GenerateLocationInvalidZip()
        {
            Location location = GenerateLocation();
            location.TaxLocation.PostalCode = "00000-0000";

            return location;
        }

        public static Location GenerateLocationBadZip()
        {
            Location location = GenerateLocation();
            location.TaxLocation.PostalCode = "!@#$%^&*()";

            return location;
        }
    }
}
