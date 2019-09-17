using AutoMapper;
using Domain.Model;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public IEnumerable<Product> Get() => _productRepository.GetAll();
 
        public Product Create(Product model)
        {
            //Validate(model);

            var createdProduct = _productRepository.Create(model);

            _unitOfWork.Complete();

            return createdProduct;
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);

            _unitOfWork.Complete();
        }

        public void Update(Product model)
        {
            //Validate(model);
            _productRepository.Update(model);
            _unitOfWork.Complete();
        }

        public Product Get(int id)
        {
            return _productRepository.GetByIdFull(id);
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return _productRepository.GetProductsByCategoryId(categoryId);
        }
    }
}
