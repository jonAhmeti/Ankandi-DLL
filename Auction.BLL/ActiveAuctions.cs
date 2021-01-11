using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.BO.Interfaces;

namespace Auction.BLL
{
    public class ActiveAuctions : ICrud<BO.ActiveAuction>
    {
        private readonly DAL.ActiveAuctions _dalActiveAuctions = new DAL.ActiveAuctions();
        public async Task<bool> AddAsync(BO.ActiveAuction obj)
        {
            return await _dalActiveAuctions.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await _dalActiveAuctions.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(BO.ActiveAuction obj)
        {
            return await _dalActiveAuctions.UpdateAsync(obj);
        }

        public async Task<BO.ActiveAuction> GetAsync(int objId)
        {
            return await _dalActiveAuctions.GetAsync(objId);
        }

        public async Task<IEnumerable<BO.ActiveAuction>> GetAllAsync()
        {
            return await _dalActiveAuctions.GetAllAsync();
        }
    }
}
