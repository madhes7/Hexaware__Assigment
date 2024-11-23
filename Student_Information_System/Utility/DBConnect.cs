using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Student_Information_System.Utility
{
    internal class DBConnect
    {
        private static readonly string Connection = "Server=DESKTOP-BAHKPDL;Database=SISDB;Trusted_Connection=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(Connection);
        }
    }
}
