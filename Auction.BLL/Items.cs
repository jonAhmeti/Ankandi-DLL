using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO;
using Auction.BO.Interfaces;
using Auction.DAL;

namespace Auction.BLL
{
    public class Items : ICrud<BO.Item>
    {
        private readonly DAL.Items _dalItems;

        public Items(DbContext context)
        {
            _dalItems = new DAL.Items(context);
        }

        public async Task<bool> AddAsync(Item obj)
        {
            return await _dalItems.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await _dalItems.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(Item obj)
        {
            return await _dalItems.UpdateAsync(obj);
        }

        public async Task<Item> GetAsync(int objId)
        {
            return await _dalItems.GetAsync(objId);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _dalItems.GetAllAsync();
        }
    }
}
