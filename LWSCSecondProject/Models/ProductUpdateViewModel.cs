using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LWSCSecondProject.Models
{
    public class ProductUpdateViewModel
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        [Remote("VarifyProduct", "Product", AdditionalFields =nameof(Id))]
        public string? Name { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Range(0.01, 99999)]
        [Display(Name = "Price (ILS)")]
        public decimal Price { get; set; }

        [Display(Name = "Cartegory")]
        public int CategoryId { get; set; }
    }
}
