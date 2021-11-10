﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class Events : ICrud<BO.Events>
    {
        private readonly DbContext _context;

        public Events(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.Events obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EventsAdd", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@StartDate", obj.StartDate);
                        command.Parameters.AddWithValue("@EndDate", obj.EndDate);
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
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EventsDeleteById", connection)
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

        public async Task<BO.Events> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EventsGetById", connection)
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

        public async Task<IEnumerable<BO.Events>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EventsGetAll", connection)
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

        public async Task<IEnumerable<BO.Events>> GetAllByAuctionId(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EventsGetByAuctionId", connection)
                        { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", objId);
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

        public async Task<bool> UpdateAsync(BO.Events obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EventsEdit", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@StartDate", obj.StartDate);
                        command.Parameters.AddWithValue("@EndDate", obj.EndDate);
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

        public async Task<List<BO.Events>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.Events> objects = new List<BO.Events>();

            while (await reader.ReadAsync())
            {
                BO.Events obj = new BO.Events
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                    EndDate = DateTime.Parse(reader["EndDate"].ToString()),
                    Lun = int.Parse(reader["LUN"].ToString()),
                    Lud = reader["LUD"] == DBNull.Value
                        ? new DateTime()
                        : DateTime.Parse(reader["LUD"].ToString()),
                    CurrentPrice = decimal.Parse(reader["CurrentPrice"].ToString()),
                    MinPriceIncrementAmount = decimal.Parse(reader["MinPriceIncrementAmount"].ToString()),
                    ItemId = int.Parse(reader["ItemId"].ToString()),
                    AuctionId = int.Parse(reader["AuctionId"].ToString())
                };

                objects.Add(obj);
            }

            return objects;
        }
    }
}
