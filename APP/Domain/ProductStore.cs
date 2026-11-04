using CORE.Domain;

namespace APP.Domain
{
    // Relational entity for the products-stores many to many relationship.
    public class ProductStore : Record
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }
    }
}
