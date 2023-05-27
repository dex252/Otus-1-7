using WebApi.Models;
using WebApi.Repositories.Abstractions;

namespace WebApi.Repositories
{
    public interface ICustomerRepository<T> : IDbRepository<T> where T : BaseEntity
    {
    }
}
