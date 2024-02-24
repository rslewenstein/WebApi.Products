using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Products.Infrastructure.Data.Interfaces;

namespace Products.Infrastructure.Data
{
    public class CreateConnection : ICreateConnection
    {
        protected readonly IConfiguration Configuration;
        public CreateConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection CreateConnectionDb()
        {
            return new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}