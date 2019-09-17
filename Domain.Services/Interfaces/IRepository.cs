using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IRepository<TEntity> 
    {
        IQueryable<TEntity> Query();

        IQueryable<TEntity> QueryEager();

        TEntity Get<TKey>(TKey id) where TKey : IComparable, IFormattable;

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        int Count();
    }

    public interface IRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
    }

    public interface IRepository
    {
    }
}
