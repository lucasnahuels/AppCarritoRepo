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
    [Route("api/v{version:apiVersion}/category")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var categoryList = _categoryService.Get();

            var categoryListMapped = _mapper.Map<List<CategoryViewModel>>(categoryList);

            return Ok(categoryListMapped);
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Category category = _categoryService.Get(id);

            if (category == null) return NotFound();

            var categoryMapped = _mapper.Map<CategoryViewModel>(category);

            return Ok(categoryMapped);
        }


        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] CategoryViewModel vm)
        {
            var categoryMapped = _mapper.Map<Category>(vm);

            var category = _categoryService.Create(categoryMapped);

            return Created("Get", vm);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] CategoryViewModel vm, int categoryId)
        {
            var categoryMapped = _mapper.Map<Category>(vm);

            categoryMapped.CategoryId = categoryId;

            _categoryService.Update(categoryMapped);

            return Ok(categoryMapped);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.Get(id);

            if (category == null) return NotFound();
  
            _categoryService.Delete(category);

            return NoContent();
        }
    }
}
