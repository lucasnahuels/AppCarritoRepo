using AutoMapper;
using Domain.Model;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace appCarrito.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/api/values")]
    [ApiVersion("2.1")]
    public class ProductController : Controller
    {
        //IProductService _productService;
        private IMapper _mapper;

        public ProductController(/* IProductService productService, */ IMapper mapper)
        {
            //_productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {

            //var product = _productService.Get();
            Seed seed = new Seed();
            List<Product> product = seed.SeedData();
                

            return Ok(_mapper.Map<List<Product>>(product));
  
        }

        //GET api/product/5
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{

        //        var product = _productService.Get(id);

        //        if (product == null)
        //        {
        //            return NotFound(id);
        //        }

        //        return Ok(_mapper.Map<Product>(product));

        //}

        ////POST api/product
        ////Creation
        //[HttpPost]
        //public IActionResult Post([FromBody]Product vm)
        //{
        //        //valdiaciones
        //        var contract = _mapper.Map<Product>(vm);
        //        var returnContract = _productService.Create(contract);

        //        return Created("Get", _mapper.Map<Product>(returnContract));

        //}

        ////PUT api/product/5
        //// Mutation
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody]Product vm)
        //{

        //        var contract = _mapper.Map<Product>(vm);
        //        contract.Id = id;
        //        _productService.Update(contract);

        //        return Ok(new { id });

        //}

        ////DELETE api/product/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //        _productService.Delete(id);
        //        return Ok();
        //}

    }
}