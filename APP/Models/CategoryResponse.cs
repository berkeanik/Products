using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class CategoryResponse : Record
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Product Count")]
        public int ProductCountR { get; set; }

        [DisplayName("Products")]
        public string ProductNamesR { get; set; }
    }
}
