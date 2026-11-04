using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class ProductRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        // decimal? is used so that Required works, otherwise the default value would be 0
        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")]
        [DisplayName("Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Range(0, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")]
        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        // int? is used so that the "-- Select --" option can't be saved
        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        // a product may not be in any store so it is not required
        [DisplayName("Stores")]
        public List<int> StoreIds { get; set; } = new();
    }
}
