using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IProductService 
    {
        Product Create(Product model);
        Product Get(int id);
        void Update(Product model);
        void Delete(Product model);
        IEnumerable<Product> Get();
        IEnumerable<Product> GetByCategory(int categoryId);

    }
}
