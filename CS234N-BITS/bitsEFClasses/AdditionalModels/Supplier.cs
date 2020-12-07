using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using bitsEFClasses.Models;
using SimpleSupplier = bitsEFClasses.Models.Supplier;
using SimpleAddress = bitsEFClasses.Models.Address;
using System.Threading.Tasks;

namespace bitsEFClasses.AdditionalModels
{
    public class Supplier
    {
        public Supplier()
        {
            Addresses = new List<Address>();
            AddressTypes = new List<AddressType>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public string Website { get; set; }
        public string Note { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<AddressType> AddressTypes { get; set; }

        public async Task FillSupplier(bitsContext dbContext, int supplierId)
        {
            SimpleSupplier s = await dbContext.Supplier.Include("SupplierAddress").Where(s => s.SupplierId == supplierId).FirstOrDefaultAsync();
            SupplierId = s.SupplierId;
            Name = s.Name;
            Phone = s.Phone;
            Email = s.Email;
            ContactFirstName = s.ContactFirstName;
            ContactLastName = s.ContactLastName;
            ContactPhone = s.ContactPhone;
            Note = s.Note;
            Website = s.Website;

            foreach (SupplierAddress sAddress in s.SupplierAddress)
            {
                int addressId = sAddress.AddressId;
                int addressTypeId = sAddress.AddressTypeId;
                SimpleAddress address = await dbContext.Address.FindAsync(addressId);
                AddressType aType = await dbContext.AddressType.FindAsync(addressTypeId);
                Addresses.Add(new Address(addressId, address.StreetLine1, address.StreetLine2, address.City,
                    address.State, address.Zipcode, addressTypeId, aType.Name));
            }
        }
    }
}
