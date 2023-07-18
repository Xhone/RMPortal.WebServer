using Microsoft.EntityFrameworkCore;

namespace RMPortal.WebServer.Models.Mpo
{
    [PrimaryKey(nameof(MpoSurId))]
    public class TxMpoSurcharge
    {       
        public int MpoSurId { get; set; }
        public string? MpoNo { get; set; }
        public string? SurType { get; set; }
        public string? SurDescription { get; set; }
        public decimal? SurPercent { get; set; }
        public decimal? SurAmount { get; set; }
        public TxMpoHd? TxMpoHd { get; set; }
    }
}
