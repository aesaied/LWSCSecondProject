using AutoMapper;
using LWSCSecondProject.Entities;
using LWSCSecondProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LWSCSecondProject.Controllers.API
{
    [Route("api/[controller]")]
     [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        private readonly IMapper _objectMapper;

        public ProductsController(MyDbContext myDbContext, IMapper mapper)
        {
            _dbContext= myDbContext;
            _objectMapper= mapper;
        }




        /// <summary>
        /// This method return all products
        /// </summary>
        /// <returns>List<Product></returns>
        //GET  api/Products/
        [HttpGet]
        public async Task<List<ProductViewModel>> GetAll()
        {
            var products= await _dbContext.Products/*Select(p=> new ProductViewModel() { Description=p.Description, Id=p.Id, Name=p.Name, Price=p.Price })*/
                    .ToListAsync();

            return _objectMapper.Map<List<ProductViewModel>>(products); 
        }

        [HttpGet("{id}")]
        //GET  api/Products/{id}
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var  product = await _dbContext.Products.Where(p=>p.Id==id).Select(p => 
            new ProductViewModel() { Description = p.Description, Id = p.Id, Name = p.Name, Price = p.Price })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();  
            }


            return  Ok(product);
        }


        //DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
              _dbContext.Remove(new Product() { Id = id });

            await _dbContext.SaveChangesAsync();    
        }


        [HttpPost]
        public async Task Post(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }


        [HttpPut("{id}")]
        // PUT api/products/5 

        public async Task<ActionResult> Put(int id,ProductUpdateViewModel input)
        {

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
           
            _objectMapper.Map(input, product);
            await _dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("getall")]
        //GET  api/Products/getAll
        public async Task<List<ProductViewModel>> GetAll2()
        {
            return await _dbContext.Products.Select(p => new ProductViewModel() { Description = p.Description, Id = p.Id, Name = p.Name, Price = p.Price }).ToListAsync();
        }
    }
}
