using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Auction.DAL
{
    public class Users : ICrud<BO.Users>
    {
        private readonly DbContext _context;

        public Users(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(BO.Users obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("UsersAdd", connection){
                    CommandType = CommandType.StoredProcedure})
                    {
                        command.Parameters.AddWithValue("@RoleId", obj.RoleId);
                        command.Parameters.AddWithValue("@Username", obj.Username);
                        command.Parameters.AddWithValue("@Password", obj.Password);
                        command.Parameters.AddWithValue("@Name", obj.Name);
                        command.Parameters.AddWithValue("@DoB", obj.DoB);

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
                    await using (SqlCommand command = new SqlCommand("UsersDeleteById", connection){
                    CommandType = CommandType.StoredProcedure})
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

        public async Task<BO.Users> GetAsync(int objId)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("UsersGetById", connection)
                        {CommandType = CommandType.StoredProcedure})
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

        public async Task<BO.Users> GetByUsernameAsync(string username)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("UsersGetByUsername", connection)
                        { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.AddWithValue("@Username", username);

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

        public async Task<IEnumerable<BO.Users>> GetAllAsync()
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("UsersGetAll", connection)
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

        public async Task<bool> UpdateAsync(BO.Users obj)
        {
            try
            {
                await using (SqlConnection connection = await _context.GetConnection())
                {
                    await using (SqlCommand command = new SqlCommand("UsersEdit", connection){
                        CommandType = CommandType.StoredProcedure})
                    {
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@RoleId", obj.RoleId);
                        command.Parameters.AddWithValue("@Username", obj.Username);
                        command.Parameters.AddWithValue("@Password", obj.Password);
                        command.Parameters.AddWithValue("@Name", obj.Name);
                        command.Parameters.AddWithValue("@DOB", obj.DoB);

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

        public async Task<List<BO.Users>> ConvertToObj(SqlDataReader reader)
        {
            List<BO.Users> objects = new List<BO.Users>();

            while (await reader.ReadAsync())
            {
                BO.Users obj = new BO.Users
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    RoleId = int.Parse(reader["RoleId"].ToString()),
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString(),
                    Name = reader["Name"].ToString(),
                    DoB = reader["DOB"] == DBNull.Value
                        ? DateTime.MinValue
                        : DateTime.Parse(reader["DOB"].ToString()),
                    Lud = DateTime.Parse(reader["LUD"].ToString()),
                    Lun = int.Parse(reader["LUN"].ToString()),
                    InD = DateTime.Parse(reader["InD"].ToString())
                };






                objects.Add(obj);
            }

            return objects;
        }
    }
}
