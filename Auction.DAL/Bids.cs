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
    public class Bids
    {
        private readonly DbContext _context;

        public Bids(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.Bids obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("BidsAdd", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
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

        public async Task<List<BO.Bids>> GetActiveAuctionBidHistory(int id)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("BidsGetByAuctionId", connection)
                        { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", id);
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

        public async Task<List<BO.Bids>> GetAllFromAuctionEventAsync(int auctionId, int eventId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("BidsGetByAuctionIdEventId", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", auctionId);
                        command.Parameters.AddWithValue("@EventId", eventId);

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

        //Get top bidder for active auction's selected event
        public async Task<List<BO.Bids>> GetTopBidderActiveEvent(int auctionId, int eventId, int userId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("BidsGetTopBidderByAuctionIdEventId", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", auctionId);
                        command.Parameters.AddWithValue("@EventId", eventId);
                        command.Parameters.AddWithValue("@UserId", userId);

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


        public async Task<List<BO.Bids>> GetLatestUserBid(int auctionId, int eventId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("BidsGetLatestByAuctionEvent", connection)
                        { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", auctionId);
                        command.Parameters.AddWithValue("@EventId", eventId);

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

        public async Task<List<BO.Bids>> ConvertToObj(SqlDataReader reader)
        {
            var objects = new List<BO.Bids>();

            while (await reader.ReadAsync())
            {
                BO.Bids obj = new BO.Bids
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    BidDate = DateTime.Parse(reader["BidDate"].ToString()),
                    BidAmount = decimal.Parse(reader["BidAmount"].ToString()),
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
