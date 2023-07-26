
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LWSCSecondProject.Entities
{
    public class Product
    {

        // Id or ProductId :primary key
        // + int : Auto increment
        public int Id { get; set; }
       
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0.01,99999)]
        public decimal Price { get; set; }  

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ProductCategory?  Category { get; set; }    
    }
}
