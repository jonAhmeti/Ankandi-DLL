using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Auction.DAL
{
    public class DbContext
    {
        private readonly string _connectionString;
        public DbContext(IConfiguration config)
        {

            _connectionString = config.GetSection("ConnectionStrings:Auction").Value;

            //ConnectionString =
            //    "Data Source=SQL5085.site4now.net;Initial Catalog=db_a76491_auction;User Id=db_a76491_auction_admin;Password=passwo0rd";
        }


        public async Task<SqlConnection> GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
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
