using System.Data;

namespace WebApi.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IDbConnection DbConnection { get; }

        public ConnectionFactory(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public IDbConnection GetConnection()
        {
            if (DbConnection.State == System.Data.ConnectionState.Closed)
            {
                DbConnection.Open();
            }

            return DbConnection;
        }
    }
}
