using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class CategoryRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Description { get; set; }
    }
}
