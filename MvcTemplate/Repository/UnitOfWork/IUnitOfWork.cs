using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
        IDbContextTransaction BeginTransaction();
    }
}
