using System.Data;

namespace Products.Infrastructure.Data.Interfaces
{
    public interface ICreateConnection
    {
        IDbConnection CreateConnectionDb();
    }
}