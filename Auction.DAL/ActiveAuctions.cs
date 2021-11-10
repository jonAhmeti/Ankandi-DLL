using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class ActiveAuctions
    {
        private readonly DbContext _context;

        public ActiveAuctions(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.ActiveAuctions obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ActiveAuctionsAdd", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                        command.Parameters.AddWithValue("@Open", obj.Open);
                        command.Parameters.AddWithValue("@OpenedBy", obj.OpenedBy);
                        command.Parameters.AddWithValue("@ClosedBy", obj.ClosedBy);

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

        public async Task<bool> DeleteAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ActiveAuctionsDeleteAll", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
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

        public async Task<BO.ActiveAuctions> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetByIdActiveAuction", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@AuctionId", objId);

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

        public async Task<IEnumerable<BO.ActiveAuctions>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ActiveAuctionsGetAll", connection)
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

        public async Task<bool> UpdateAsync(BO.ActiveAuctions obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EditActiveAuction", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                        command.Parameters.AddWithValue("@Opened", obj.Open);
                        command.Parameters.AddWithValue("@OpenedBy", obj.OpenedBy);

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

        public async Task<bool> Open(int userId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ActiveAuctionsOpen", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
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

        public async Task<bool> Close(int userId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ActiveAuctionsClose", connection)
                    { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
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

        public async Task<List<BO.ActiveAuctions>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.ActiveAuctions> objects = new List<BO.ActiveAuctions>();

            while (await reader.ReadAsync())
            {
                BO.ActiveAuctions obj = new BO.ActiveAuctions
                {
                    AuctionId = int.Parse(reader["AuctionId"].ToString()),
                    Open = bool.Parse(reader["Open"].ToString()),
                    OpenedBy = int.Parse(reader["OpenedBy"].ToString()),
                    ClosedBy = int.Parse(reader["ClosedBy"].ToString())
                };

                objects.Add(obj);
            }

            return objects;
        }
    }
}
