using System;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IUnitOfWork
    {
        int Complete();
    }
}
