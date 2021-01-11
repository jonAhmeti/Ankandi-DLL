using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public class BidHistory
    {
        public int Id { get; set; }
        public DateTime BidDate { get; set; }
        public decimal BidAmount { get; set; }
        public int EventId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }
}
