using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Auction.DAL;

namespace Auction.BLL
{
    public class Users : ICrud<BO.Users>
    {
        private readonly DAL.Users _dalUsers;

        public Users(DbContext context)
        {
            _dalUsers = new DAL.Users(context);
        }
        public async Task<bool> AddAsync(BO.Users obj)
        {
            return await _dalUsers.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await _dalUsers.DeleteAsync(objId);
        }

        public async Task<IEnumerable<BO.Users>> GetAllAsync()
        {
            return await _dalUsers.GetAllAsync();
        }

        public async Task<BO.Users> GetAsync(int objId)
        {
            return await _dalUsers.GetAsync(objId);
        }

        public async Task<BO.Users> GetByUsernameAsync(string username)
        {
            return await _dalUsers.GetByUsernameAsync(username);
        }

        public async Task<bool> UpdateAsync(BO.Users obj)
        {
            return await _dalUsers.UpdateAsync(obj);
        }
    }
}
