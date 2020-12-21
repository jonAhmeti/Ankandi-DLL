using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class Items : ICrud<BO.Item>
    {
        public async Task<bool> AddAsync(BO.Item obj)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("AddItem", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@StartPrice", obj.StartPrice);
                        command.Parameters.AddWithValue("@Details", obj.Details);
                        command.Parameters.AddWithValue("@SoldPrice", obj.SoldPrice);
                        command.Parameters.AddWithValue("@SoldDate", obj.SoldDate);
                        command.Parameters.AddWithValue("@Name", obj.Name);
                        command.Parameters.AddWithValue("@Units", obj.MeasurementUnits);
                        command.Parameters.AddWithValue("@Amount", obj.Amount);

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
                    await using (SqlCommand command = new SqlCommand("DeleteByIdItem", connection)
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

        public async Task<BO.Item> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetByIdItem", connection)
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

        public async Task<IEnumerable<BO.Item>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("GetListItems", connection)
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

        public async Task<bool> UpdateAsync(BO.Item obj)
        {
            try
            {
                await using (SqlConnection connection = await DbContext.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("EditItem", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@StartPrice", obj.StartPrice);
                        command.Parameters.AddWithValue("@Details", obj.Details);
                        command.Parameters.AddWithValue("@SoldPrice", obj.SoldPrice);
                        command.Parameters.AddWithValue("@SoldDate", obj.SoldDate);
                        command.Parameters.AddWithValue("@Name", obj.Name);
                        command.Parameters.AddWithValue("@Units", obj.MeasurementUnits);
                        command.Parameters.AddWithValue("@Amount", obj.Amount);

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

        public async Task<List<BO.Item>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.Item> objects = new List<BO.Item>();

            while (await reader.ReadAsync())
            {
                BO.Item obj = new BO.Item
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    StartPrice = decimal.Parse(reader["StartPrice"].ToString()),
                    Details = reader["Details"].ToString(),
                    SoldPrice = reader["SoldPrice"] == DBNull.Value
                        ? -1
                        : decimal.Parse(reader["SoldPrice"].ToString()),
                    SoldDate = reader["SoldDate"] == DBNull.Value
                        ? DateTime.MinValue
                        : DateTime.Parse(reader["SoldDate"].ToString()),
                    Name = reader["Name"].ToString(),
                    MeasurementUnits = reader["MeasurementUnits"].ToString(),
                    Amount = double.Parse(reader["Amount"].ToString()),
                    InD = reader["InD"] == DBNull.Value
                        ? DateTime.MinValue
                        : DateTime.Parse(reader["InD"].ToString()),
                    Lud = reader["LUD"] == DBNull.Value
                        ? DateTime.MinValue
                        : DateTime.Parse(reader["LUD"].ToString()),
                    Lun = reader["LUN"] == DBNull.Value
                        ? -1
                        : int.Parse(reader["LUN"].ToString()),
                    Image = reader["Image"] == DBNull.Value
                        ? ""
                        : reader["Image"].ToString()
                };










                objects.Add(obj);
            }

            return objects;
        }
    }
}
