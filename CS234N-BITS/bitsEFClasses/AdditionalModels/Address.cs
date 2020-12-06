using System;
using System.Collections.Generic;
using System.Text;

namespace bitsEFClasses.AdditionalModels
{
    public class Address
    {
        public Address()
        {
        }
        public Address(int addressId, string streetLine1, string streetLine2, string city, string state, string zip, int typeId, string typeName)
        {
            AddressId = addressId;
            StreetLine1 = streetLine1;
            StreetLine2 = streetLine2;
            City = city;
            State = state;
            Zipcode = zip;
            TypeId = typeId;
            TypeName = typeName;
        }

        public int AddressId { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
