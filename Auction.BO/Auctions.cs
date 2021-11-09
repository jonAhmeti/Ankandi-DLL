using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class Auctions
    {
        public Auctions()
        {
            ActiveAuctions = new HashSet<ActiveAuctions>();
            Bids = new HashSet<Bids>();
            Events = new HashSet<Events>();
            Withdrawals = new HashSet<Withdrawals>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<ActiveAuctions> ActiveAuctions { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Events> Events { get; set; }
        public virtual ICollection<Withdrawals> Withdrawals { get; set; }
    }
}
