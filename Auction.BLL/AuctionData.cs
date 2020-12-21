using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO.Interfaces;

namespace Auction.BLL
{
    public class AuctionData : ICrud<BO.AuctionData>
    {
        private readonly DAL.AuctionData dalAuctionData = new DAL.AuctionData();
        public async Task<bool> AddAsync(BO.AuctionData obj)
        {
            return await dalAuctionData.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await dalAuctionData.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(BO.AuctionData obj)
        {
            return await dalAuctionData.UpdateAsync(obj);
        }

        public async Task<BO.AuctionData> GetAsync(int objId)
        {
            return await dalAuctionData.GetAsync(objId);
        }

        public async Task<IEnumerable<BO.AuctionData>> GetAllAsync()
        {
            return await dalAuctionData.GetAllAsync();
        }
    }
}
