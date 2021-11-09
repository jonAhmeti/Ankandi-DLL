using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class ActiveAuctions
    {
        public int AuctionId { get; set; }
        public bool Open { get; set; }
        public int OpenedBy { get; set; }
        public int ClosedBy { get; set; }
        public int Id { get; set; }

        public virtual Auctions Auction { get; set; }
        public virtual Users ClosedByNavigation { get; set; }
        public virtual Users OpenedByNavigation { get; set; }
    }
}
