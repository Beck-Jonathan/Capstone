using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class DBConnectionProvider
    {
        static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Night_Rider;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
