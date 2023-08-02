using AutoMapper;
using LWSCSecondProject.Entities;
using LWSCSecondProject.Models;

namespace LWSCSecondProject
{
    public class AutoMapperConfig :Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();

        }
    }
}
