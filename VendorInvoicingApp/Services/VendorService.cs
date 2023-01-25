using BoaIm_Assignment3.Entities;
using BoaIm_Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BoaIm_Assignment3.Services
{
    public class VendorService : IVendorService
    {
        public VendorService(VendorDbContext vendorDbContext)
        {
            _vendorDbContext= vendorDbContext;
        }

        public List<Vendor> getAllVendors(string lowerBound, string upperBound)
        {
            return _vendorDbContext.Vendors.Where(m => m.Name.ToLower().Substring(0, 1).CompareTo(lowerBound) >= 0 && m.Name.ToLower().Substring(0, 1).CompareTo(upperBound) <= 0)
                .OrderBy(m => m.Name).ToList();
        }

        public Vendor findVendorbyId(int id)
        {
            return _vendorDbContext.Vendors.Find(id);
        }

        public int AddNewVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Add(vendor);
            _vendorDbContext.SaveChanges();
            return vendor.VendorId;
        }

        public int updateVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Update(vendor);
            _vendorDbContext.SaveChanges();
            return vendor.VendorId;
        }

        public Vendor findVendorbyIdWithInvoice(int id)
        {
            return _vendorDbContext.Vendors.Where(m => m.VendorId == id).Include(m => m.Invoices).FirstOrDefault();
        }

        public Invoice findInvoicebyIdWithLineItem(int id)
        {
            return _vendorDbContext.Invoices.Where(m => m.VendorId == id).Include(m => m.InvoiceLineItems).FirstOrDefault();
        }

        public Invoice findInvoicebyInvoiceIdWithLineItem(int id)
        {
            return _vendorDbContext.Invoices.Where(m => m.InvoiceId == id).Include(m => m.InvoiceLineItems).FirstOrDefault();
            
        }

        public void deleteVender(Vendor vendor)
        {
            _vendorDbContext.Vendors.Remove(vendor);
            _vendorDbContext.SaveChanges();
        }

        public List<PaymentTerms> getAllPaymentsTerms()
        {
            return _vendorDbContext.paymentTerms.OrderBy(g => g.PaymentTermsId).ToList();
        }

        public void AddNewInvoice(Vendor vendor, Invoice invoice)
        {
            vendor.Invoices.Add(invoice);
            _vendorDbContext.SaveChanges();
        }

        public void AddNewLineItem(Invoice invoice, InvoiceLineItem lineItem)
        {
            invoice.InvoiceLineItems.Add(lineItem);
            _vendorDbContext.SaveChanges();
        }

        private VendorDbContext _vendorDbContext;
    }
}
