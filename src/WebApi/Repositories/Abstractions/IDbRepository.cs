using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories.Abstractions
{
    public interface IDbRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(long id);
        Task<long> CreateAsync(T model);
    }
}
