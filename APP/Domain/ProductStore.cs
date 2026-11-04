using CORE.Domain;

namespace APP.Domain
{
    // products-stores many to many relationship entity
    public class ProductStore : Record
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }
    }
}
