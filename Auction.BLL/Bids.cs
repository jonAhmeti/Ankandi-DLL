using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.DAL;

namespace Auction.BLL
{
    public class Bids
    {
        private readonly DAL.Bids dalBidHistories;

        public Bids(DbContext context)
        {
            dalBidHistories = new DAL.Bids(context);
        }

        public async Task<bool> AddAsync(BO.Bids obj)
        {
            return await dalBidHistories.AddAsync(obj);
        }

        public async Task<IEnumerable<BO.Bids>> GetActiveAuctionBidHistory(int id)
        {
            return await dalBidHistories.GetActiveAuctionBidHistory(id);
        }

        public async Task<IEnumerable<BO.Bids>> GetAllFromAuctionEventAsync(int auctionId, int eventId)
        {
            return await dalBidHistories.GetAllFromAuctionEventAsync(auctionId, eventId);
        }

        public async Task<IEnumerable<BO.Bids>> GetTopBidderActiveEvent(int auctionId, int eventId, int userId)
        {
            return await dalBidHistories.GetTopBidderActiveEvent(auctionId, eventId, userId);
        }

        public async Task<List<BO.Bids>> GetLatestUserBid(int auctionId, int eventId)
        {
            return await dalBidHistories.GetLatestUserBid(auctionId, eventId);
        }
    }
}
