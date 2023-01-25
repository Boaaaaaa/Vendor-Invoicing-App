using System.ComponentModel.DataAnnotations;

namespace BoaIm_Assignment3.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Please enter a vendor's name.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a vendor's address.")]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required(ErrorMessage = "Please enter the city.")]
        public string? City { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the province or state.")]
        [StringLength(2, ErrorMessage = "Must be 2 characters long.")]
        public string? ProvinceOrState { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the zip code or postal code.")]
        [RegularExpression("(^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Invalid zip or postal code.")]
        public string? ZipOrPostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        public string? VendorPhone { get; set; }

        [Required(ErrorMessage = "Please enter the contact last name.")]
        public string? VendorContactLastName { get; set; }

        [Required(ErrorMessage = "Please enter the contact first name.")]
        public string? VendorContactFirstName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? VendorContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}
