using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Auction.DAL
{
    public static class DbContext
    {
        static DbContext()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var appSettings = configBuilder.Build()
                .GetSection("ConnectionStrings:Auction");
            ConnectionString = appSettings.Value;
        }

        private static readonly string ConnectionString;

        public static async Task<SqlConnection> GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
