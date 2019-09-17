using Domain.Model;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services.Infrastructure
{
    public class ProductRepository : Repository<Product, DataBaseContext>, IProductRepository
    {
        public ProductRepository(DataBaseContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public int Add(Product product) => Create(product).ProductId;

        public void Remove(Product product) => Delete(product);

        public void Edit(Product product) => Update(product);

        public IEnumerable<Product> GetAll() => Query().ToList();

        public Product GetByIdFull(int id) => QueryEager().Where(_ => _.ProductId == id).FirstOrDefault();

        public override IQueryable<Product> QueryEager()
        {
            return Query().Include(x => x.Category);
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return QueryEager().Where(x => x.Category.CategoryId == categoryId).ToList();
        }



    }
}

