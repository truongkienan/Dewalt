using System.Data;
using System.Data.SqlClient;

namespace Dewalt.Models
{
    public class BaseProvider: IDisposable
    {
        IDbConnection connection;        
        string connectionString;

        public BaseProvider(IConfiguration configuration)
        {            
            connectionString = configuration.GetConnectionString("Dewalt");            
        }

        protected IDbConnection Connection
        {
            get
            {
                if (connection is null)
                {
                    Console.WriteLine("Da tao ket noi*****");
                    //connection = new SqlConnection(configuration.GetConnectionString("Dewalt"));
                    connection=new SqlConnection(connectionString);
                }
                return connection;
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}
