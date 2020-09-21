using DapperFluent.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Api.Cores.SqlDbProviders
{
    public interface ISqlClientDbProvider : IDbProviders<SqlConnection>
    {
    }

    public sealed class SqlClientDbProvider : ISqlClientDbProvider
    {
        private IDapperBuilder dapperBuilder = null;
        private IConfiguration configuration = null;

        public SqlClientDbProvider(IDapperBuilder dapperBuilder, IConfiguration configuration)
        {
            this.dapperBuilder = dapperBuilder;
            this.configuration = configuration;
        }

        IDapperBuilder IDbProviders<SqlConnection>.DapperBuilder => dapperBuilder;

        SqlConnection IDbProviders<SqlConnection>.GetConnection()
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }
    }
}