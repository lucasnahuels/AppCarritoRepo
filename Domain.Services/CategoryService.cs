using Domain.Model;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> Get() => _categoryRepository.GetAll();

        public Category Create(Category model)
        {
            //Validate(model);

            var createdCategory = _categoryRepository.Create(model);

            _unitOfWork.Complete();

            return createdCategory;
        }

        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);

            _unitOfWork.Complete();
        }

        public void Update(Category model)
        {
            //Validate(model);
            _categoryRepository.Update(model);
            _unitOfWork.Complete();
        }

        public Category Get(int id)
        {
            return _categoryRepository.GetByIdFull(id);
        }
    }
}
