using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class StoreResponse : Record
    {
        public string Name { get; set; }

        public bool IsVirtual { get; set; }

        [DisplayName("Virtual")]
        public string IsVirtualR { get; set; }

        [DisplayName("Product Count")]
        public int ProductCountR { get; set; }

        [DisplayName("Products")]
        public string ProductNamesR { get; set; }
    }
}
