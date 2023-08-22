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
    public class ProductCategoriesController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        private readonly IMapper _objectMapper;

        public ProductCategoriesController(MyDbContext myDbContext, IMapper mapper)
        {
            _dbContext = myDbContext;
            _objectMapper = mapper;
        }



        [HttpGet]
        public async Task< List<CategoryViewModel>> GetAll()
        {
            var data = await _dbContext.ProductCategories.ToListAsync();

            return _objectMapper.Map<List<CategoryViewModel>>(data);

        }


    }
}
