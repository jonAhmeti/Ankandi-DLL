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
            public async Task<bool> AddAsync(BO.ActiveAuction obj)
            {
                try
                {
                    await using (SqlConnection connection = await _context.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("AddActiveAuction", connection)
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

            public async Task<bool> DeleteAsync()
            {
                try
                {
                    await using (SqlConnection connection = await _context.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("DeleteActiveAuction", connection)
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

            public async Task<BO.ActiveAuction> GetAsync(int objId)
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

            public async Task<IEnumerable<BO.ActiveAuction>> GetAllAsync()
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

            public async Task<bool> UpdateAsync(BO.ActiveAuction obj)
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

            public async Task<bool> Open()
            {
                try
                {
                    await using (SqlConnection connection = await _context.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("ActiveAuctionOpen", connection)
                            { CommandType = CommandType.StoredProcedure })
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

            public async Task<bool> Close()
            {
                try
                {
                    await using (SqlConnection connection = await _context.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("ActiveAuctionClose", connection)
                            { CommandType = CommandType.StoredProcedure })
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

        public async Task<List<BO.ActiveAuction>> ConvertToObj(SqlDataReader reader)
            {
                List<BO.ActiveAuction> objects = new List<BO.ActiveAuction>();

                while (await reader.ReadAsync())
                {
                    BO.ActiveAuction obj = new BO.ActiveAuction
                    {
                        AuctionId = int.Parse(reader["AuctionId"].ToString()),
                        Open = bool.Parse(reader["Opened"].ToString()),
                        OpenedBy = int.Parse(reader["OpenedBy"].ToString())
                    };

                    objects.Add(obj);
                }

                return objects;
            }
        }
}
