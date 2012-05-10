using System;
using System.Collections.Generic;

namespace InformacionPaisesBackEnd
{
    public class AirportComparer : IEqualityComparer<Airport>
    {
        public bool Equals(Airport x, Airport y)
        {
            //Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal.
            return x.Code == y.Code && x.Name == y.Name;
        }

        public int GetHashCode(Airport obj)
        {
            //Check whether the object is null
            if (ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            //Get hash code for the Code field.
            int hashAirportCode = obj.Code.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashAirportCode;
        }
    }
}
