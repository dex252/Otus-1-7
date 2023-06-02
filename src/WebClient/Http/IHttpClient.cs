using System.Threading.Tasks;

namespace WebClient.Http
{
    internal interface IHttpClient
    {
        Task<long> CreateCustomerAsync<T>(T request);
        Task<Customer> GetCustomerAsync<T>(T id);
    }
}
