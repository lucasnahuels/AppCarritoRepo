using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByIdFull(int id);

        IEnumerable<Category> GetAll();

        int Add(Category category);

        void Edit(Category category);

        void Remove(Category category);

    }
}
