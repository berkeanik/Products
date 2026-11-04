using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Category : Record
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // categories-products one to many relationship
        public List<Product> Products { get; set; } = new();
    }
}
