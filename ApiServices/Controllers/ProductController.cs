using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiServices.Controllers
{
    [Route("api/v{version:apiVersion}/product")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var productList = _productService.Get();

            var productListMapped = _mapper.Map<List<ProductViewModel>>(productList);

            return Ok(productListMapped);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Product product = _productService.Get(id);

            if (product == null) return NotFound();
            
            var productMapped = _mapper.Map<ReadProductViewModel>(product);

            if (product.Category != null)
            productMapped.CategoryName = product.Category.Name;

            return Ok(productMapped);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] CreateProductViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(); //?????????????????????????

            //Category category = _categoryService.Get(vm.CategoryId);

            //if (category == null) return BadRequest();

            var productMapped = _mapper.Map<Product>(vm);

            //productMapped.Category = category;

            Product product = _productService.Create(productMapped);
            
            return Created("Get", product.ProductId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] CreateProductViewModel vm, int id)
        {
            var productMapped = _mapper.Map<Product>(vm);

            productMapped.Category = _categoryService.Get(vm.CategoryId);

            productMapped.ProductId = id;

            _productService.Update(productMapped);

            return Ok(vm);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _productService.Get(id);

            if (product == null) return NotFound();

            _productService.Delete(product);

            return NoContent();
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetByCategory(int categoryId)
        {
            Category category = _categoryService.Get(categoryId);

            if (category == null) return NotFound();

            var productList = _productService.GetByCategory(categoryId);

            var productListMapped = _mapper.Map<List<ReadProductViewModel>>(productList);

            foreach (Product product in productList)
            {
                if (product.Category != null)
                {
                    foreach (ReadProductViewModel productMapped in productListMapped)
                    {
                        productMapped.CategoryName = product.Category.Name;
                    }
                }
            }

            return Ok(productListMapped);
        }
    }
}
