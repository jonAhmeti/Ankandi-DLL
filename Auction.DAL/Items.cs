using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class Items : ICrud<BO.Items>
    {
        private readonly DbContext _context;

        public Items(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.Items obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ItemsAdd", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@StartPrice", obj.StartPrice);
                        command.Parameters.AddWithValue("@Name", obj.Name);
                        command.Parameters.AddWithValue("@Details", obj.Details);
                        command.Parameters.AddWithValue("@SoldPrice", obj.SoldPrice);
                        command.Parameters.AddWithValue("@SoldDate", obj.SoldDate);
                        command.Parameters.AddWithValue("@MeasurementUnit", obj.MeasurementUnit);
                        command.Parameters.AddWithValue("@Amount", obj.Amount);
                        command.Parameters.AddWithValue("@Image", obj.Image);

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
                    await using (SqlCommand command = new SqlCommand("ItemsDeleteById", connection)
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

        public async Task<BO.Items> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ItemsGetById", connection)
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

        public async Task<IEnumerable<BO.Items>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ItemsGetAll", connection)
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

        public async Task<bool> UpdateAsync(BO.Items obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("ItemsEdit", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@Name", obj.Name);
                        command.Parameters.AddWithValue("@StartPrice", obj.StartPrice);
                        command.Parameters.AddWithValue("@MeasurementUnit", obj.MeasurementUnit);
                        command.Parameters.AddWithValue("@Amount", obj.Amount);
                        command.Parameters.AddWithValue("@SoldDate", obj.SoldDate);
                        command.Parameters.AddWithValue("@SoldPrice", obj.SoldPrice);
                        command.Parameters.AddWithValue("@Image", obj.Image);
                        command.Parameters.AddWithValue("@Details", obj.Details);

                        var result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<BO.Items>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.Items> objects = new List<BO.Items>();

            while (await reader.ReadAsync())
            {
                BO.Items obj = new BO.Items
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    StartPrice = decimal.Parse(reader["StartPrice"].ToString()),
                    MeasurementUnit = reader["MeasurementUnit"].ToString(),
                    Amount = int.Parse(reader["Amount"].ToString()),
                    SoldDate = reader["SoldDate"] == DBNull.Value ?
                        new DateTime?() : DateTime.Parse(reader["SoldDate"].ToString()),
                    SoldPrice = reader["SoldPrice"] == DBNull.Value ?
                        new decimal() : decimal.Parse(reader["SoldPrice"].ToString()),
                    Image = reader["Image"] == DBNull.Value ?
                        "" : reader["Image"].ToString(),
                    Details = reader["Details"].ToString(),
                    InD = reader["InD"] == DBNull.Value ?
                        new DateTime() : DateTime.Parse(reader["InD"].ToString()),
                    Lud = reader["LUD"] == DBNull.Value ?
                        new DateTime() : DateTime.Parse(reader["LUD"].ToString()),
                    Lun = reader["LUN"] == DBNull.Value ?
                        new int() : int.Parse(reader["LUN"].ToString()),

                };

                objects.Add(obj);
            }

            return objects;
        }
    }
}
