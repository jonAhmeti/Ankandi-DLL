using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class Withdrawals
    {
        public int Id { get; set; }
        public DateTime WithdrawDate { get; set; }
        public decimal WithdrawAmount { get; set; }
        public int EventId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }

        public virtual Auctions Auction { get; set; }
        public virtual Events Event { get; set; }
        public virtual Users User { get; set; }
    }
}
