using System.ComponentModel.DataAnnotations;

namespace Seller.API.Models
{
    public class Seller
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Pin { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
