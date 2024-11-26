using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Student_Information_System.Utility
{
    internal class DBConnect
    {

        private static IConfiguration _iconfiguration;

        static DBConnect()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
        }

        public static string GetConnectionString()
        {

            return _iconfiguration.GetConnectionString("LocalConnectionString");

        }
    }
}
