using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class ActiveAuctions : ICrud<BO.ActiveAuction>
        {
            public async Task<bool> AddAsync(BO.ActiveAuction obj)
            {
                try
                {
                    await using (SqlConnection connection = await DbContext.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("AddActiveAuction", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                            command.Parameters.AddWithValue("@Opened", obj.Opened);
                            command.Parameters.AddWithValue("@OpenedBy", obj.OpenedBy);
                            command.Parameters.AddWithValue("@Closed", obj.Closed);
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

            public async Task<bool> DeleteAsync(int objId)
            {
                try
                {
                    await using (SqlConnection connection = await DbContext.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("DeleteActiveAuction", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            command.Parameters.AddWithValue("@AuctionId", objId);

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
                    await using (SqlConnection connection = await DbContext.GetConnection())
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
                    await using (SqlConnection connection = await DbContext.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("GetListActiveAuctions", connection)
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
                    await using (SqlConnection connection = await DbContext.GetConnection())
                    {
                        await using (SqlCommand command = new SqlCommand("EditActiveAuction", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            command.Parameters.AddWithValue("@AuctionId", obj.AuctionId);
                            command.Parameters.AddWithValue("@Opened", obj.Opened);
                            command.Parameters.AddWithValue("@OpenedBy", obj.OpenedBy);
                            command.Parameters.AddWithValue("@Closed", obj.Closed);
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

            public async Task<List<BO.ActiveAuction>> ConvertToObj(SqlDataReader reader)
            {
                List<BO.ActiveAuction> objects = new List<BO.ActiveAuction>();

                while (await reader.ReadAsync())
                {
                    BO.ActiveAuction obj = new BO.ActiveAuction
                    {
                        AuctionId = int.Parse(reader["AuctionId"].ToString()),
                        Opened = bool.Parse(reader["Opened"].ToString()),
                        OpenedBy = int.Parse(reader["OpenedBy"].ToString()),
                        Closed = bool.Parse(reader["Closed"].ToString()),
                        ClosedBy = int.Parse(reader["ClosedBy"].ToString())
                    };

                    objects.Add(obj);
                }

                return objects;
            }
        }
}
