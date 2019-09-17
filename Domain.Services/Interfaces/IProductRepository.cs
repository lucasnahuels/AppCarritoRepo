using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByIdFull(int id);

        IEnumerable<Product> GetAll();

        int Add(Product product);

        void Edit(Product product);

        void Remove(Product product);

        IEnumerable<Product> GetProductsByCategoryId(int categoryId);

    }
}
