using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FishingStoreApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 20, ErrorMessage = "Display Order must be between 1-20.")]
        public int DisplayOrder { get; set; }

    }
}
