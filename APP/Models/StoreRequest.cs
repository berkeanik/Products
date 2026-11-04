using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class StoreRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [DisplayName("Virtual")]
        public bool IsVirtual { get; set; }
    }
}
