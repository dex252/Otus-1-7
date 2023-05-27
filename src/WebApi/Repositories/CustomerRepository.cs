using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Threading.Tasks;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class CustomerRepository<T> : ICustomerRepository<T> where T : BaseEntity
    {
        private DbContext DbContext { get; }
        public CustomerRepository (DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<long> CreateAsync(T model)
        {
            try
            {
                var result = await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == model.Id);
                if (result != null)
                {
                    throw new CustomException(Enums.ErrorType.Conflict, $"Пользователь с id={model.Id} уже существует");
                }

                await DbContext.Set<T>().AddAsync(model);
                return await DbContext.SaveChangesAsync();
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }           
        }


        public async Task<T> GetAsync(long id)
        {
            try
            {
                var result = await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                if (result == null)
                {
                    throw new CustomException(Enums.ErrorType.NotFound, $"Пользователь с id={id} не найден");
                }

                return result;
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
           
        }
    }
}
