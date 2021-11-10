using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Auction.DAL;

namespace Auction.BLL
{
    public class ActiveAuctions
    {
        private readonly DAL.ActiveAuctions _dalActiveAuctions;

        public ActiveAuctions(DbContext context)
        {
            _dalActiveAuctions = new DAL.ActiveAuctions(context);
        }

        public async Task<bool> AddAsync(BO.ActiveAuctions obj)
        {
            return await _dalActiveAuctions.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync()
        {
            return await _dalActiveAuctions.DeleteAsync();
        }

        public async Task<bool> UpdateAsync(BO.ActiveAuctions obj)
        {
            return await _dalActiveAuctions.UpdateAsync(obj);
        }

        public async Task<BO.ActiveAuctions> GetAsync(int objId)
        {
            return await _dalActiveAuctions.GetAsync(objId);
        }

        public async Task<IEnumerable<BO.ActiveAuctions>> GetAllAsync()
        {
            return await _dalActiveAuctions.GetAllAsync();
        }
        public async Task<bool> Open(int userId)
        {
            return await _dalActiveAuctions.Open(userId);
        }
        public async Task<bool> Close(int userId)
        {
            return await _dalActiveAuctions.Close(userId);
        }



    }
}
