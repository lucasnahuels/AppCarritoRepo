using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using ApiServices.Swagger;
using Domain.Model;
using Domain.Services.Interfaces;
using Domain.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Services;
using Microsoft.Extensions.Configuration;

namespace RestApi.Example.Utils.Swagger
{

    /// <summary>
    /// Service Collection(IServiceCollection) Extensions
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Add AddVersionedApiExplorer and AddApiVersioning middlewares
        /// </summary>
        /// <param name="services"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddApiVersionWithExplorer(this IServiceCollection services)
        {
            return services
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
        }

        /// <summary>
        /// Add swagger services
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>/></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSwaggerOptions(this IServiceCollection services)
        {
            return services
                .AddTransient<IConfigureOptions<SwaggerOptions>, ConfigureSwaggerOptions>()
                .AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUiOptions>()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        }

        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<DataBaseContext>>();
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));
        }

    }

}