using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class AuctionData
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Bids> BidHistory { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<WithdrawHistory> WithdrawHistory { get; set; }
    }
}
