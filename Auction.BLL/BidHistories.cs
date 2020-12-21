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
        public async Task<IEnumerable<BO.BidHistory>> GetAllFromEventAsync(BO.BidHistory obj)
        {
            return await dalBidHistories.GetAllFromEventAsync(obj);
        }
    }
}
