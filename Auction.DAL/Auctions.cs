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
    public class Auctions: ICrud<BO.Auctions>
    {
        private readonly DbContext _context;

        public Auctions(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.Auctions obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AuctionsAdd", connection)
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
                    await using (SqlCommand command = new SqlCommand("AuctionsDeleteById", connection)
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

        public async Task<BO.Auctions> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AuctionsGetById", connection)
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

        public async Task<IEnumerable<BO.Auctions>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AuctionsGetAll", connection)
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

        public async Task<bool> UpdateAsync(BO.Auctions obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AuctionsEdit", connection)
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

        public async Task<List<BO.Auctions>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.Auctions> objects = new List<BO.Auctions>();

            while (await reader.ReadAsync())
            {
                BO.Auctions obj = new BO.Auctions
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
