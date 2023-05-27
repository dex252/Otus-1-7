using System.Data;

namespace WebApi.Factories
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
