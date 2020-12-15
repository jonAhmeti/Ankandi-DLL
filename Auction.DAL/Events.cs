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
    public class Events : ICrud<BO.Event>
    {
        public async Task<bool> AddAsync(BO.Event obj)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AddEvent", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@StartDate", obj.StartDate);
                        command.Parameters.AddWithValue("@EndDate", obj.EndDate);
                        command.Parameters.AddWithValue("@TopBidder", obj.TopBidder);
                        command.Parameters.AddWithValue("@CurrentPrice", obj.CurrentPrice);
                        command.Parameters.AddWithValue("@MinPriceIncrementAmount", obj.MinPriceIncrementAmount);
                        command.Parameters.AddWithValue("@ItemId", obj.ItemId);
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);

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

        public async Task<bool> DeleteAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("DeleteByIdEvent", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@Id", objId);

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

        public async Task<BO.Event> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetByIdEvent", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@Id", objId);

                        await using SqlDataReader reader = command.ExecuteReader();
                        return (await ConvertToObj(reader)).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<IEnumerable<BO.Event>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetListEvents", connection)
                    { CommandType = CommandType.StoredProcedure })
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

        public async Task<bool> UpdateAsync(BO.Event obj)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EditEvent", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@StartDate", obj.StartDate);
                        command.Parameters.AddWithValue("@EndDate", obj.EndDate);
                        command.Parameters.AddWithValue("@TopBidder", obj.TopBidder);
                        command.Parameters.AddWithValue("@CurrentPrice", obj.CurrentPrice);
                        command.Parameters.AddWithValue("@MinPriceIncrementAmount", obj.MinPriceIncrementAmount);
                        command.Parameters.AddWithValue("@ItemId", obj.ItemId);
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);

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

        public async Task<List<BO.Event>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.Event> objects = new List<BO.Event>();

            while (await reader.ReadAsync())
            {
                BO.Event obj = new BO.Event();

                obj.Id = int.Parse(reader["Id"].ToString());

                obj.StartDate = reader["StartDate"] == DBNull.Value
                    ? new DateTime?()
                    : DateTime.Parse(reader["StartDate"].ToString());

                obj.EndDate = reader["EndDate"] == DBNull.Value
                    ? new DateTime?()
                    : DateTime.Parse(reader["EndDate"].ToString());

                obj.Lun = reader["LUN"] == DBNull.Value
                    ? new int?()
                    : int.Parse(reader["LUN"].ToString());

                obj.Lud = reader["LUD"] == DBNull.Value
                    ? new DateTime?()
                    : DateTime.Parse(reader["LUD"].ToString());

                obj.TopBidder = reader["TopBidder"] == DBNull.Value
                    ? new int?()
                    : int.Parse(reader["TopBidder"].ToString());

                obj.CurrentPrice = decimal.Parse(reader["CurrentPrice"].ToString());
                obj.MinPriceIncrementAmount = decimal.Parse(reader["MinPriceIncrementAmount"].ToString());

                obj.ItemId = reader["ItemId"] == DBNull.Value
                    ? -1
                    : int.Parse(reader["ItemId"].ToString());

                obj.AuctionId = reader["AuctionId"] == DBNull.Value
                    ? -1
                    : int.Parse(reader["AuctionId"].ToString());

                objects.Add(obj);
            }

            return objects;
        }
    }
}
