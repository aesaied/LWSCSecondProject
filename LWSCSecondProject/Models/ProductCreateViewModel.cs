using LWSCSecondProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Humanizer.Localisation;

namespace LWSCSecondProject.Models
{
    public class ProductCreateViewModel :IValidatableObject
    {

      

        [Required]
        [MaxLength(100)]
        [Display(Name= "Name")]
        [Remote("VarifyProduct", "Product")]
        public string? Name { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Range(0.01, 99999)]
        [Display(Name = "Price (ILS)")]
        public decimal Price { get; set; }

        [Display(Name = "Cartegory")]
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.ToLower().Contains("test"))
            {
                yield return new ValidationResult(
                    $"Product name can't contains 'test' keyword!",
                    new[] { nameof(Name) });
            }
        }
    }
}
