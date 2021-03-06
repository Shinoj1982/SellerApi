using System.ComponentModel.DataAnnotations;

namespace Seller.API.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }
}
