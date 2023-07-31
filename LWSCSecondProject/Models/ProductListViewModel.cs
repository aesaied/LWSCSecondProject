using LWSCSecondProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LWSCSecondProject.Models
{
    public class ProductListViewModel
    {
        public int Id { get; set; }

       
        public string? Name { get; set; }

     
        public string? Description { get; set; }

       
        public decimal Price { get; set; }

      

       
        public string  CategoryName { get; set; }
    }
}
