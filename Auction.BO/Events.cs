using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class Events
    {
        public Events()
        {
            Bids = new HashSet<Bids>();
            Withdrawals = new HashSet<Withdrawals>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AuctionId { get; set; }
        public int ItemId { get; set; }
        public DateTime Lud { get; set; }
        public int Lun { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal MinPriceIncrementAmount { get; set; }

        public virtual Auctions Auction { get; set; }
        public virtual Items Item { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Withdrawals> Withdrawals { get; set; }
    }
}
