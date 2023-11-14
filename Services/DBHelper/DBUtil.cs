using System.Data.SqlClient;

namespace StudentsDbApp.Services.DBHelper
{
    public class DBUtil
    {
        private DBUtil() { }

        public static SqlConnection? GetConnection()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            string? connString = configuration.GetConnectionString("defaultConnection");

            try
            {
                SqlConnection conn = new(connString);
                return conn;
            }catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return null;
            }
        }
    }
}
