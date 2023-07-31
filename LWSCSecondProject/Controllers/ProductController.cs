using LWSCSecondProject.Entities;
using LWSCSecondProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace LWSCSecondProject.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly MyDbContext _dbContext;

        public ProductController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string? name)
        {
            //Method Syntax
            var products =  _dbContext.Products.AsQueryable();


            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(s => s.Name.Contains(name));
            }


            //select 
            // Execute Query
            var  productList =await products.Select(p=>new ProductListViewModel() { Id=p.Id, CategoryName=p.Category.Name, Description=p.Description, Name=p.Name, Price=p.Price }).ToListAsync();
            //Query Syntax (LINQ)

            //  select  a.*  from  products a where a.Name like 'a%'
            //var  products2= await (from a in _dbContext.Products select a).ToListAsync();

            return View(productList);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
           await FillLookups();
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {

            var  product = await _dbContext.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }


            var output = new ProductUpdateViewModel() { Id = product.Id, CategoryId = product.CategoryId, Description = product.Description, Name = product.Name, Price = product.Price };

            await FillLookups();

            return View(output);    


        }


        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel input)
        {

            if (ModelState.IsValid)
            {
                Product product = new() { Id= input.Id, CategoryId=input.CategoryId, Description= input.Description, Name= input.Name,  Price=input.Price };

                _dbContext.Update(product); //  state Modified
                 await _dbContext.SaveChangesAsync();


                TempData["MSG"] = $"Product updated successfuly";
                return RedirectToAction("Index");
            }

            await FillLookups();

            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel input)
        { 
            //  Server side validation  
            if(ModelState.IsValid)
            {
                //  custom validation 

                try
                {

                    if (! IsExists(input.Name))
                    {

                        Product product = new() { CategoryId = input.CategoryId, Name = input.Name, Description = input.Description, Price = input.Price };
                        _dbContext.Products.Add(product);

                        // Save data to  database
                        await _dbContext.SaveChangesAsync();

                        TempData["MSG"] = $"Product added successfuly";
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        ModelState.AddModelError(nameof(ProductCreateViewModel.Name), "Name already used!");
                    }
                   

                }
                catch
                {

                    ModelState.AddModelError("", "An error occured, Please try again!");
                }
            }



            await FillLookups();
            return View(input);
        }


        public IActionResult VarifyProduct(string name, int? id)
        {
            if (IsExists(name, id))
            {
                return Json($"Name '{name}' is already in use.");
            }

            return Json(true);
        }

        private bool IsExists(string name, int? id=null)
        {
            return   _dbContext.Products.Any(s=>s.Name== name && s.Id!=id); 
        }

        private async Task FillLookups()
        {
            var categories = await _dbContext.ProductCategories.ToListAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(int? id)
        {
            var product  = new Product { Id = id??0};

            _dbContext.Remove(product);
            _dbContext.SaveChanges();

            return Json(true);
        }
    }
}
