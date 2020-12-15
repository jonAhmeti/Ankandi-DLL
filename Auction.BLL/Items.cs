using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO;
using Auction.BO.Interfaces;

namespace Auction.BLL
{
    class Items : ICrud<BO.Item>
    {
        private readonly DAL.Items dalItems = new DAL.Items();

        public async Task<bool> AddAsync(Item obj)
        {
            return await dalItems.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await dalItems.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(Item obj)
        {
            return await dalItems.UpdateAsync(obj);
        }

        public async Task<Item> GetAsync(int objId)
        {
            return await dalItems.GetAsync(objId);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await dalItems.GetAllAsync();
        }
    }
}
