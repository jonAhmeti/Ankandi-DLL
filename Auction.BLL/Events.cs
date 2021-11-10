using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO;
using Auction.BO.Interfaces;
using Auction.DAL;

namespace Auction.BLL
{
    public class Events : ICrud<BO.Events>
    {
        private readonly DAL.Events _dalEvents;

        public Events(DbContext context)
        {
            _dalEvents = new DAL.Events(context);
        }
        public async Task<bool> AddAsync(BO.Events obj)
        {
            return await _dalEvents.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await _dalEvents.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(BO.Events obj)
        {
            return await _dalEvents.UpdateAsync(obj);
        }

        public async Task<BO.Events> GetAsync(int objId)
        {
            return await _dalEvents.GetAsync(objId);
        }

        public async Task<IEnumerable<BO.Events>> GetAllAsync()
        {
            return await _dalEvents.GetAllAsync();
        }

        public async Task<IEnumerable<BO.Events>> GetAllByAuctionId(int objId)
        {
            return await _dalEvents.GetAllByAuctionId(objId);
        }
    }
}
