using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class Withdraws
    {
        private readonly DbContext _context;

        public Withdraws(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.WithdrawHistory obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AddWithdraw", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@BidderId", obj.UserId);
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                        command.Parameters.AddWithValue("@EventId", obj.EventId);
                        command.Parameters.AddWithValue("@Date", obj.WithdrawDate);
                        command.Parameters.AddWithValue("@Amount", obj.WithdrawAmount);

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

        public async Task<BO.Bids> GetThrowbackAsync(BO.WithdrawHistory obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetWithdrawThrowback", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@BidderId", obj.UserId);
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                        command.Parameters.AddWithValue("@EventId", obj.EventId);

                        await using SqlDataReader reader = command.ExecuteReader();
                        var objBidHistory = new BO.Bids();
                        while (await reader.ReadAsync())
                        {
                            objBidHistory.Id = int.Parse(reader["Id"].ToString());
                            objBidHistory.AuctionId = int.Parse(reader["AuctionId"].ToString());
                            objBidHistory.EventId = int.Parse(reader["EventId"].ToString());
                            objBidHistory.UserId = int.Parse(reader["UserId"].ToString());
                            objBidHistory.BidAmount = decimal.Parse(reader["BidAmount"].ToString());
                            objBidHistory.BidDate = DateTime.Parse(reader["BidDate"].ToString());
                        }

                        return objBidHistory;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public async Task<List<BO.WithdrawHistory>> GetActiveAuctionWithdrawHistory()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetActiveAuctionWithdrawHistory", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
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


        public async Task<List<BO.WithdrawHistory>> ConvertToObj(SqlDataReader reader)
        {
            var objects = new List<BO.WithdrawHistory>();

            while (await reader.ReadAsync())
            {
                BO.WithdrawHistory obj = new BO.WithdrawHistory()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    WithdrawDate = DateTime.Parse(reader["WithdrawDate"].ToString()),
                    WithdrawAmount = decimal.Parse(reader["WithdrawAmount"].ToString()),
                    EventId = int.Parse(reader["EventId"].ToString()),
                    AuctionId = int.Parse(reader["AuctionId"].ToString()),
                    UserId = int.Parse(reader["UserId"].ToString())
                };

                objects.Add(obj);
            }

            return objects;
        }
    }
}
