
using System.ComponentModel.DataAnnotations;

namespace LWSCSecondProject.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        
        public string? Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
