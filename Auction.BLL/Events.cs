using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO;
using Auction.BO.Interfaces;

namespace Auction.BLL
{
    public class Events : ICrud<BO.Event>
    {
        private readonly DAL.Events dalEvents = new DAL.Events();
        public async Task<bool> AddAsync(Event obj)
        {
            return await dalEvents.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await dalEvents.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(Event obj)
        {
            return await dalEvents.UpdateAsync(obj);
        }

        public async Task<Event> GetAsync(int objId)
        {
            return await dalEvents.GetAsync(objId);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await dalEvents.GetAllAsync();
        }

        public async Task<IEnumerable<Event>> GetAllByAuctionId(int objId)
        {
            return await dalEvents.GetAllByAuctionId(objId);
        }
    }
}
