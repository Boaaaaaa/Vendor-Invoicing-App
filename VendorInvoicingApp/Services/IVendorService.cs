using BoaIm_Assignment3.Entities;

namespace BoaIm_Assignment3.Services
{
    public interface IVendorService
    {
        public List<Vendor> getAllVendors(string lowerBound, string upperBound);
        public Vendor findVendorbyId(int id);
        public int AddNewVendor(Vendor vendor);
        public int updateVendor(Vendor vendor);
        public Vendor findVendorbyIdWithInvoice(int id);
        public void deleteVender(Vendor vendor);
        public Invoice findInvoicebyIdWithLineItem(int id);
        public Invoice findInvoicebyInvoiceIdWithLineItem(int id);
        public List<PaymentTerms> getAllPaymentsTerms();
        public void AddNewInvoice (Vendor vendor, Invoice invoice);
        public void AddNewLineItem(Invoice invoice, InvoiceLineItem lineItem);
    }
}
