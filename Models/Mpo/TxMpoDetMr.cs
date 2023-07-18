using Microsoft.EntityFrameworkCore;

namespace RMPortal.WebServer.Models.Mpo
{
    [PrimaryKey(nameof(MpoDetJobId))]
    public class TxMpoDetMr
    {
        public string? MpoNo { get; set; }  
        public int MpoDetId { get; set; }
        public int MpoDetJobId { get; set; }
        public string? MrNo { get; set; }
        public decimal? Qty { get; set; }

        public  TxMpoDet? TxMpoDet { get; set; }
    }
}
