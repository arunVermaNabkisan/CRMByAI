using System.Data;

namespace SambhandhCRM.Core.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
