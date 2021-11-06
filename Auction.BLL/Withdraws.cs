using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Auction.BO;
using Auction.DAL;

namespace Auction.BLL
{
    public class Withdraws
    {
        private readonly DAL.Withdraws _dalWithdraw;

        public Withdraws(DbContext context)
        {
            _dalWithdraw = new DAL.Withdraws(context);
        }

        public async Task<bool> AddAsync(BO.WithdrawHistory obj)
        {
            return await _dalWithdraw.AddAsync(obj);
        }

        public async Task<List<WithdrawHistory>> GetActiveAuctionWithdrawHistory()
        {
            return await _dalWithdraw.GetActiveAuctionWithdrawHistory();
        }
        public async Task<Bids> GetThrowbackAsync(BO.WithdrawHistory obj)
        {
            return await _dalWithdraw.GetThrowbackAsync(obj);
        }
    }
}
