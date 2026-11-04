using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Store : Record
    {
        [Required, StringLength(150)]
        public string Name { get; set; }

        public bool IsVirtual { get; set; }

        // stores-products many to many relationship
        public List<ProductStore> ProductStores { get; set; } = new();
    }
}
