using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Seller.API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string ProductName { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string DetailedDescription { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        [Required]
        public decimal StartingPrice { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "BidEndDate should be in future")]
        public DateTime BidEndDate { get; set; }

        [Required]
        public Seller Seller { get; set; } 
    }
}
