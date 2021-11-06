namespace Auction.BO
{
    public class ActiveAuction
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public bool Open { get; set; }
        public int OpenedBy { get; set; }
        public int ClosedBy { get; set; }
    }
}
