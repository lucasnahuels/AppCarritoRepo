using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AutoMapper;
using Domain.Services.Interfaces;
using Domain.Services;
using Domain.Model;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            AddUnitOfWork(services);
            //AddExternalServices(services);
            AddServices(services);

            //services.AddAutoMapper();

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork<DataBaseContext>>(); //crear DataBaseContext
            //services.AddSingleton<IProductService, ProductService>();

        }

        //private static void AddExternalServices(IServiceCollection services)
        //{

        //}

        private static void AddUnitOfWork(IServiceCollection services)
        {

        }
    }
}