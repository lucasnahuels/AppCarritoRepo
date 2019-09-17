using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ICategoryService 
    {
        Category Create(Category model);
        Category Get(int id);
        void Update(Category model);
        void Delete(Category model);
        IEnumerable<Category> Get();

    }
}
