using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.BO.Interfaces;
using Auction.DAL;

namespace Auction.BLL
{
    public class Auctions : ICrud<BO.Auctions>
    {
        private readonly DAL.Auctions dalAuctionData;

        public Auctions(DbContext context)
        {
            dalAuctionData = new DAL.Auctions(context);
        }
        public async Task<bool> AddAsync(BO.Auctions obj)
        {
            return await dalAuctionData.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int objId)
        {
            return await dalAuctionData.DeleteAsync(objId);
        }

        public async Task<bool> UpdateAsync(BO.Auctions obj)
        {
            return await dalAuctionData.UpdateAsync(obj);
        }

        public async Task<BO.Auctions> GetAsync(int objId)
        {
            return await dalAuctionData.GetAsync(objId);
        }

        public async Task<IEnumerable<BO.Auctions>> GetAllAsync()
        {
            return await dalAuctionData.GetAllAsync();
        }
    }
}
