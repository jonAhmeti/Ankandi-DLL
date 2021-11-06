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
    public class AuctionData: ICrud<BO.AuctionData>
    {
        private readonly DbContext _context;

        public AuctionData(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.AuctionData obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AddAuctionData", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@StartDate", obj.StartDate);
                        command.Parameters.AddWithValue("@EndDate", obj.EndDate);

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
                    await using (SqlCommand command = new SqlCommand("DeleteByIdAuctionData", connection)
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

        public async Task<BO.AuctionData> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetByIdAuctionData", connection)
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

        public async Task<IEnumerable<BO.AuctionData>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetListAuctionData", connection)
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

        public async Task<bool> UpdateAsync(BO.AuctionData obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EditAuctionData", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@StartDate", obj.StartDate);
                        command.Parameters.AddWithValue("@EndDate", obj.EndDate);

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

        public async Task<List<BO.AuctionData>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.AuctionData> objects = new List<BO.AuctionData>();

            while (await reader.ReadAsync())
            {
                BO.AuctionData obj = new BO.AuctionData
                {
                    Id = int.Parse(reader["Id"].ToString()),

                    StartDate = DateTime.Parse(reader["StartDate"].ToString()),

                    EndDate = DateTime.Parse(reader["EndDate"].ToString())

                    //
                    //EndDate = reader["EndDate"] == DBNull.Value
                    //? new DateTime?()
                    //: DateTime.Parse(reader["EndDate"].ToString())
                };

                objects.Add(obj);
            }

            return objects;
        }
    }
}
