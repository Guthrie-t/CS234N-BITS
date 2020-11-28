using System.Collections.Generic;
using System.Linq;
using System;
using NUnit.Framework;

using bitsEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BITSUnitTests
{
    [TestFixture]
    public class SupplierTests
    {
        bitsContext dbContext;
        Supplier s;
        List<Supplier> suppliers;
        SupplierAddress sa;
        List<SupplierAddress> sAddresses;
        List<Address> al;
        Address a;
        AddressType at;


        [SetUp]
        public void Setup()
        {
            dbContext = new bitsContext();
            //if you haven't added my usp to your database, comment this out
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetbitsData()"); 
        }

        [Test]
        public void GetAllSuppliersTest()
        {
            suppliers = dbContext.Supplier.OrderBy(s => s.SupplierId).ToList();
            Assert.AreEqual(6, suppliers.Count);
            Assert.AreEqual("Hopsteiner", suppliers[3].Name);
            PrintAll(suppliers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Supplier.Find(4); //SupplierId is primary key for Supplier
            Assert.IsNotNull(s);
            Console.WriteLine(s);

            //SupplierAddress requires all three field values to find using Find, better to use Where
            sAddresses = dbContext.SupplierAddress.Where(s => s.SupplierId == 4).ToList(); //This finds all address codes for supplier 4
            Assert.IsNotNull(sAddresses);
            PrintAllSA(sAddresses);

            a = dbContext.Address.Find(4); //this finds the address associated with address code 4
            Assert.IsNotNull(a);
            Console.WriteLine(a);
        }

        [Test]
        public void TestJoinSuppliertoSupplierAddress()
        {
            var supplierCodes = dbContext.Supplier.Join( //select * from Supplier
                dbContext.SupplierAddress, //join SupplierAddress on Supplier.SupplierId = SupplierAddress.SupplierId
                s => s.SupplierId,
                sa => sa.SupplierId,
                //and then return these fields below from those tables where the SupplierId is equal to 4
                (s, sa) => new { s.SupplierId, s.Name, sa.AddressId, sa.AddressTypeId }).Where(s => s.SupplierId == 4).ToList();
            Assert.AreEqual(2, supplierCodes.Count);
            Console.WriteLine(supplierCodes[0]);
        }

        [Test]
        public void TestGetAddressFromSupplierId()
        {
            //This first variable joins the Supplier and SupplierAddress table to return a Billing Address Id for the specified 
            //Supplier. You can alter what type of address is returned by altering the AddressTypeId
            var supplierCodes = dbContext.Supplier.Join(
                dbContext.SupplierAddress,
                s => s.SupplierId,
                sa => sa.SupplierId,
                (s, sa) => new { s.SupplierId, s.Name, sa.AddressId, sa.AddressTypeId })
                    .Where(s => s.SupplierId == 4 && s.AddressTypeId ==1 ).ToList();
            var addressId =  supplierCodes[0].AddressId;

            //Two joins in one statement is apparently very difficult with anonymous object types. Easier to do two and just
            //store the info we need in a variable.

            //addressId is used as a parameter here in the second join to return ONLY the requested address for the supplier above. 
            var billAddress = dbContext.SupplierAddress.Join(
                dbContext.Address,
                a => a.AddressId,
                sa => sa.AddressId,
                (sa, a) => new {sa.AddressId, a.StreetLine1, a.StreetLine2, a.City, a.State, a.Zipcode, a.Country })
                .Where(sa => sa.AddressId == addressId).ToList();

            var addressCity = billAddress[0].City; //when using ToList() you have to indicate what piece of the list to look at thus [0]

            Assert.AreEqual("New York", addressCity); //could also have been billAddress[0].City
        }

        public void PrintAll(List<Supplier> suppliers)
        {
            foreach (Supplier s in suppliers)
            {
                Console.WriteLine(s);
            }
        }
        public void PrintAllSA(List<SupplierAddress> Addresses)
        {
            foreach (SupplierAddress s in Addresses)
            {
                Console.WriteLine(s);
            }
        }
    }
}