using Domain.Model;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services.Infrastructure
{
    public class CategoryRepository : Repository<Category, DataBaseContext>, ICategoryRepository
    {
        public CategoryRepository(DataBaseContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
            
        }

        public int Add(Category category) => Create(category).CategoryId;

        public void Remove(Category category) => Delete(category);

        public void Edit(Category category) => Update(category);

        public IEnumerable<Category> GetAll() => Query().ToList();

        public Category GetByIdFull(int id) => Get(id);

        public override IQueryable<Category> QueryEager()
        {
            return Query().Include(x => x.Products);
        }
    }
}

