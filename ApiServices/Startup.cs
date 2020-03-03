using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServices.Swagger;
using AutoMapper;
using Domain.Services.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestApi.Example.Utils.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;


namespace ApiServices
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Default",
                builder =>
                {
                    builder.WithOrigins("https://localhost:44338");
                });
            });

            services.AddAutoMapper(typeof(GeneralProfile));

            services.AddDomainServices();

            services.AddDatabaseContext(Configuration.GetConnectionString("Seed"));

            services.AddSingleton(Configuration);

            services.Configure<SwaggerSettings>(Configuration.GetSection(nameof(SwaggerSettings)));

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddApiVersionWithExplorer()
                .AddSwaggerOptions()
                .AddSwaggerGen();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors((builder) => builder.WithOrigins("https://localhost:44338").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //.AllowAnyMethod().AllowAnyMethod().AllowAnyOrigin());

            app.UseSwaggerDocuments();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
