using AutoMapper;
using DataTables.AspNetCore.Mvc.Binder;
using LWSCSecondProject.Entities;
using LWSCSecondProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Linq.Dynamic.Core;

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

        [HttpGet("GetPagedResult")]
        public async Task<PagedResultViewModel<ProductViewModel>> GetPagedResult([FromQuery] DataTableFilter filter) 
        {


            var draw = filter.Draw; //Request.Form["draw"].FirstOrDefault();
            var sortColumn = filter.SortColumn; //  Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = filter.SortDirection; //Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = filter.SearchText; //Request.Form["search[value]"].FirstOrDefault();
            int pageSize = filter.PageSize;//Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = filter.SkipCount; // Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            if(pageSize==0) pageSize = 10;
            
            var products =  _dbContext.Products.AsQueryable();

            var totalRecords = await products.CountAsync();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                products = products.Where(p => p.Name.Contains(searchValue) || p.Description.Contains(searchValue)) ;
            }

            //  do filter

            var filteredRecords = await products.CountAsync();

            if (string.IsNullOrWhiteSpace(sortColumn)) sortColumn = "id";

            var pagedData = await products.OrderBy($"{sortColumn} {sortColumnDirection}").Skip(skip).Take(pageSize).ToListAsync();

            /*Select(p=> new ProductViewModel() { Description=p.Description, Id=p.Id, Name=p.Name, Price=p.Price })*/


            var  data =_objectMapper.Map<List<ProductViewModel>>(pagedData);


            return new PagedResultViewModel<ProductViewModel>() { RecordsTotal = totalRecords, RecordsFiltered = filteredRecords, Data = data };
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
