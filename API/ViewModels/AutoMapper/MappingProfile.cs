using API.Data.EntityBase.Entities;
using API.ViewModels.Categories;
using API.ViewModels.Orders;
using API.ViewModels.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryVm>().ReverseMap();
            CreateMap<Product, ViewProduct>().ReverseMap();
            CreateMap<Product, CreateProduct>().ReverseMap();
            CreateMap<Order, CreateOrder>().ReverseMap();
            CreateMap<Order, ViewOrder>().ReverseMap();
            CreateMap<Order, ViewOrderBasic>().ReverseMap();
        }
    }
}
