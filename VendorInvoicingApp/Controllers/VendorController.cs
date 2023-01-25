using BoaIm_Assignment3.Entities;
using BoaIm_Assignment3.Models;
using BoaIm_Assignment3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Numerics;

namespace BoaIm_Assignment3.Controllers
{
    public class VendorController : Controller
    {
        public VendorController(IVendorService vendorService, VendorDbContext vendorDbContext)
        {
            _vendorService = vendorService;
            _vendorDbContext = vendorDbContext;
        }

        [HttpGet("/vendors/groups/{lowerBound}-{upperBound}")]
        public IActionResult GetAllVendors(string lowerBound, string upperBound)
        {
            var vendor = _vendorService.getAllVendors(lowerBound, upperBound);

            if (lowerBound == "A")
            {
                ViewData["buttonClass1"] = "active";

            }
            else if (lowerBound == "F")
            {
                ViewData["buttonClass2"] = "active";
            }
            else if (lowerBound == "L")
            {
                ViewData["buttonClass3"] = "active";

            }
            else if (lowerBound == "S")
            {
                ViewData["buttonClass4"] = "active";
            }

            return View("Vendors", vendor);
        }

        [HttpGet("/vendors/group/A-E")]
        public IActionResult Vendors()
        {
            return GetAllVendors("A", "E");
        }

        [HttpGet("/vendors/add-request")]
        public IActionResult GetAddVendorRequest()
        {
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                ActiveVendor = new Vendor()
            };
            return View("Add", vendorViewModel);
        }

        [HttpPost("/vendors")]
        public IActionResult AddNewVendor(VendorViewModel vendorViewModel)
        {
            if (ModelState.IsValid)
            {
                _vendorService.AddNewVendor(vendorViewModel.ActiveVendor);

                TempData["LastActionMessage"] = $"The vendor \"{vendorViewModel.ActiveVendor.Name}\" was added succesfully";

                return RedirectToAction("Vendors", "Vendor");
            }
            else
            {
                return View("Add", vendorViewModel);
            }
        }

        [HttpGet("/vendors/{id}/edit-request")]
        public IActionResult GetEditRequestById(int id)
        {
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                ActiveVendor = _vendorService.findVendorbyId(id)
            };

            return View("Edit", vendorViewModel);
        }

        [HttpPost("/vendors/edit-request")]
        public IActionResult EditTheVendor(VendorViewModel vendorViewModel)
        {
            if (ModelState.IsValid)
            {
                _vendorService.updateVendor(vendorViewModel.ActiveVendor);

                TempData["LastActionMessage"] = $"The vendor \"{vendorViewModel.ActiveVendor.Name}\" was edited succesfully";

                return RedirectToAction("Vendors", "Vendor");
            }
            else
            {
                return View("Edit", vendorViewModel);
            }
        }

        [HttpGet("/vendors/{id}/delete-request")]
        public IActionResult GetDeleteRequestById(int id)
        {
            var vendor = _vendorService.findVendorbyIdWithInvoice(id);

            return DeleteVendor(vendor);
        }

        [HttpPost("/vendors/delete-request")]
        public IActionResult DeleteVendor(Vendor vendor)
        {
            _vendorService.deleteVender(vendor);
            TempData["LastActionMessageUndo"] = $"The vendor \"{vendor.Name}\" was deleted succesfully!!! <a href=\"#\">UNDO</a>";
            return RedirectToAction("Vendors", "Vendor");
        }

        [HttpGet("/vendors/{id}")]
        public IActionResult GetVendorById(int id)
        {
            var vendor = _vendorService.findVendorbyIdWithInvoice(id);
            var invoice = _vendorService.findInvoicebyIdWithLineItem(id);

            InvoiceViewModel invoiceViewModel = new InvoiceViewModel()
            {
                ActiveVendor = vendor,
                ActiveInvoice = invoice,
                paymentTerms = _vendorDbContext.paymentTerms.OrderBy(g => g.PaymentTermsId).ToList()
            };

            return View("Invoices", invoiceViewModel);
        }

        [HttpGet("/vendors/{vendorid}/invoices/{invoiceid}")]
        public IActionResult GetInvoiceById(int vendorid, int invoiceid)
        {
            var vendor = _vendorService.findVendorbyIdWithInvoice(vendorid);
            var invoice = _vendorService.findInvoicebyInvoiceIdWithLineItem(invoiceid);

            InvoiceViewModel invoiceViewModel = new InvoiceViewModel()
            {
                ActiveVendor = vendor,
                ActiveInvoice = invoice,
                paymentTerms = _vendorService.getAllPaymentsTerms()
            };

            ViewData["highlight"] = "table-primary";

            return View("Invoices", invoiceViewModel);
        }

        [HttpPost("/vendors/{vendorid}/invoices/{invoiceid}/add-invoice-request")]
        public IActionResult AddInvoiceToVendorById(int vendorid, int invoiceid, InvoiceViewModel invoiceViewModel)
        {
            var vendor = _vendorService.findVendorbyIdWithInvoice(vendorid);
            int invoiceId = invoiceid;
            _vendorService.AddNewInvoice(vendor, invoiceViewModel.NewInvoice);

            return GetInvoiceById(vendor.VendorId, invoiceId);
        }

        [HttpPost("/vendors/{vendorid}/invoices/{invoiceid}/add-lineitem-request")]
        public IActionResult AddLineItemToInvoiceById(int vendorid, int invoiceid, InvoiceViewModel invoiceViewModel)
        {
            var invoice = _vendorService.findInvoicebyInvoiceIdWithLineItem(invoiceid);
            _vendorService.AddNewLineItem(invoice, invoiceViewModel.NewLineItem);

            return GetInvoiceById(invoice.VendorId, invoice.InvoiceId);
        }

        private IVendorService _vendorService;
        private VendorDbContext _vendorDbContext;
    }
}
