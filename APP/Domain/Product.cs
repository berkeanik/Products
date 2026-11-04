using CORE.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    public class Product : Record
    {
        [Required, StringLength(150)]
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int? StockAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        // products-category one to many relationship
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        // products-stores many to many relationship
        public List<ProductStore> ProductStores { get; set; } = new();

        // Not a column in the Products table, used to get and set the store ids of the product.
        [NotMapped]
        public List<int> StoreIds
        {
            get => ProductStores.Select(productStore => productStore.StoreId).ToList();
            set => ProductStores = value.Select(storeId => new ProductStore { StoreId = storeId }).ToList();
        }
    }
}
