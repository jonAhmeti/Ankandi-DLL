using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public class BidHistories
    {
        private readonly DAL.BidHistories dalBidHistories = new DAL.BidHistories();

        public async Task<bool> AddAsync(BO.BidHistory obj)
        {
            return await dalBidHistories.AddAsync(obj);
        }
        public async Task<IEnumerable<BO.BidHistory>> GetAllFromAuctionEventAsync(BO.BidHistory obj)
        {
            return await dalBidHistories.GetAllFromAuctionEventAsync(obj);
        }

        public async Task<BO.BidHistory> GetLatestUserBid(int userId, int auctionId, int eventId)
        {
            return await dalBidHistories.GetLatestUserBid(userId, auctionId, eventId);
        }
    }
}
