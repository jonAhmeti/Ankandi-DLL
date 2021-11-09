using System;
using System.Collections.Generic;

namespace Auction.BO
{
    public partial class Users
    {
        public Users()
        {
            ActiveAuctionsClosedByNavigation = new HashSet<ActiveAuctions>();
            ActiveAuctionsOpenedByNavigation = new HashSet<ActiveAuctions>();
            Bids = new HashSet<Bids>();
            Withdrawals = new HashSet<Withdrawals>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public DateTime? Lud { get; set; }
        public int Lun { get; set; }
        public DateTime InD { get; set; }
        public int RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<ActiveAuctions> ActiveAuctionsClosedByNavigation { get; set; }
        public virtual ICollection<ActiveAuctions> ActiveAuctionsOpenedByNavigation { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Withdrawals> Withdrawals { get; set; }
    }
}
