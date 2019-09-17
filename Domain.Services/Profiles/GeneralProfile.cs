using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
             CreateMap<CategoryViewModel, Category>(); // para ir a la bd

             CreateMap<ProductViewModel, Product>();

             CreateMap<BillViewModel, Bill>();

            CreateMap<Category, CategoryViewModel>(); // para traer de la bd

            CreateMap<Product, ProductViewModel>();

            CreateMap<Bill,  BillViewModel>();

            CreateMap<CreateProductViewModel, Product>();

            CreateMap<Product, ReadProductViewModel>();

        }
    }
}
