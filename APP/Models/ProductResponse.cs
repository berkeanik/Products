using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class ProductResponse : Record
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int? StockAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int CategoryId { get; set; }

        public List<int> StoreIds { get; set; }

        [DisplayName("Unit Price")]
        public string UnitPriceR { get; set; }

        [DisplayName("Stock Amount")]
        public string StockAmountR { get; set; }

        [DisplayName("Expiration Date")]
        public string ExpirationDateR { get; set; }

        [DisplayName("Category")]
        public string CategoryTitleR { get; set; }

        [DisplayName("Stores")]
        public string StoreNamesR { get; set; }
    }
}
