using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class BidHistories
    {
        public async Task<bool> AddAsync(BO.BidHistory obj)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AddBid", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@BidDate", obj.BidDate);
                        command.Parameters.AddWithValue("@BidAmount", obj.BidAmount);
                        command.Parameters.AddWithValue("@EventId", obj.EventId);
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                        command.Parameters.AddWithValue("@UserId", obj.UserId);

                        return await command.ExecuteNonQueryAsync() != -1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<BO.BidHistory>> GetAllFromEventAsync(BO.BidHistory obj)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetBidHistory", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                        command.Parameters.AddWithValue("@EventId", obj.EventId);

                        await using SqlDataReader reader = command.ExecuteReader();
                        return await ConvertToObj(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<BO.BidHistory>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.BidHistory> objects = new List<BO.BidHistory>();

            while (await reader.ReadAsync())
            {
                BO.BidHistory obj = new BO.BidHistory
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    
                    BidDate = reader["BidDate"] == DBNull.Value 
                        ? new DateTime?()
                        : DateTime.Parse(reader["BidDate"].ToString()),

                    BidAmount = decimal.Parse(reader["BidDate"].ToString()),

                    EventId = reader["EventId"] == DBNull.Value
                        ? new int?()
                        : int.Parse(reader["EventId"].ToString()),
                    AuctionId = reader["AuctionId"] == DBNull.Value
                        ? new int?()
                        : int.Parse(reader["AuctionId"].ToString()),

                    UserId = int.Parse(reader["UserId"].ToString())
                };

                objects.Add(obj);
            }

            return objects;
        }
    }
}
