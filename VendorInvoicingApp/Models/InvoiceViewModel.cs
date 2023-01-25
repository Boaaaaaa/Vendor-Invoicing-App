using BoaIm_Assignment3.Entities;

namespace BoaIm_Assignment3.Models
{
    public class InvoiceViewModel
    {
        public Vendor ActiveVendor { get; set; }
        public Invoice ActiveInvoice { get; set; }
        public List<PaymentTerms> paymentTerms { get; set; }
        public InvoiceLineItem NewLineItem { get; set; }
        public Invoice NewInvoice { get; set; }
    }
}
