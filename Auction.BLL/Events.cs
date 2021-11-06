using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO;
using Auction.BO.Interfaces;
using Auction.DAL;

namespace Auction.BLL
{
    public class Events : ICrud<BO.Event>
    {
        private readonly DAL.Events _dalEvents;

        public Events(DbContext context)
        {
            _dalEvents = new DAL.Events(context);
        }
        public async Task<bool> AddAsync(Event obj)
        {
            return await _dalEvents.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await _dalEvents.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(Event obj)
        {
            return await _dalEvents.UpdateAsync(obj);
        }

        public async Task<Event> GetAsync(int objId)
        {
            return await _dalEvents.GetAsync(objId);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _dalEvents.GetAllAsync();
        }

        public async Task<IEnumerable<Event>> GetAllByAuctionId(int objId)
        {
            return await _dalEvents.GetAllByAuctionId(objId);
        }
    }
}
