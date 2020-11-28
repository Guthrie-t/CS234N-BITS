using System.Collections.Generic;
using System.Linq;
using System;
using NUnit.Framework;

using bitsEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BITSUnitTests
{
    public class Tests
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
            var supplierCodes = dbContext.Supplier.Join(
                dbContext.SupplierAddress,
                s => s.SupplierId,
                sa => sa.SupplierId,
                (s, sa) => new { s.SupplierId, s.Name, sa.AddressId, sa.AddressTypeId }).Where(s => s.SupplierId == 4).ToList();
            Assert.AreEqual(2, supplierCodes.Count);
        }

        [Test]
        public void GetAddressFromSupplierId()
        {


            var address =
             (from s in dbContext.Supplier
             join sa in dbContext.SupplierAddress
                 on s.SupplierId equals sa.SupplierId
             join a in dbContext.Address
                 on a.AddressId equals sa.AddressId
             where s.SupplierId == 4
             where sa.AddressTypeId == 1
              select new
             {
                 s.SupplierId,
                 s.Name,
                 a.StreetLine1,
                 a.StreetLine2,
                 a.City,
                 a.State,
                 a.Zipcode,
                 a.Country
             });

            Assert.IsNotNull(address);
            //I can't figure out how to look at the data in this to be sure it's real. --TG

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