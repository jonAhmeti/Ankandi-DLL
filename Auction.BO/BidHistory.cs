﻿using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class BidHistory
    {
        public int Id { get; set; }
        public DateTime? BidDate { get; set; }
        public decimal BidAmount { get; set; }
        public int? EventId { get; set; }
        public int? AuctionId { get; set; }
        public int UserId { get; set; }

        public virtual AuctionData Auction { get; set; }
        public virtual Event Event { get; set; }
        public virtual Users User { get; set; }
    }
}
