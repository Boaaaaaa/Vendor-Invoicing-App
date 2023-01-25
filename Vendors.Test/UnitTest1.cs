using BoaIm_Assignment3.Entities;

namespace Vendors.Test
{
    public class UnitTest1
    {
        [Fact]
        public void AddingIvoice()
        {
            Vendor vendor = new Vendor()
            {
                Name = "Some Corp",
                Address1 = "sfaeowif;ad",
                City = "Toronto",
                ProvinceOrState = "ON",
                ZipOrPostalCode = "3M61N2",
                VendorPhone = "1212121122",
                VendorContactLastName = "Im",
                VendorContactFirstName = "Boa"
            };

            vendor.Invoices.Add(new Invoice() { InvoiceDate = DateTime.Today});
        }
    }
}