using System.Data;

namespace WebApi.Products.Infrastructure.Data.Interfaces
{
    public interface ICreateConnection
    {
        IDbConnection CreateConnectionDb();
    }
}