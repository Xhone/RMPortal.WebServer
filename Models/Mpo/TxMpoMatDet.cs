using Microsoft.EntityFrameworkCore;

namespace RMPortal.WebServer.Models.Mpo
{
    [PrimaryKey (nameof(MpoMatDetId))]
    public class TxMpoMatDet
    {
        public int MpoMatDetId { get; set; }
        public string? MpoNo { get; set; }
        public string? MatCode { get; set; }
        public string? Remark { get; set; } 
        public string? BuyUnit { get; set; }
        public decimal? BuyUnitFactor {  get; set; }
        public string? MatDesc { get; set; }
        public string? PriceUnit { get; set; }
        public decimal? PriceUnitFactor { get;set; }
        public string? TempMat { get; set; }
        public decimal? Width { get; set; }
        public decimal? Weight { get; set; } = 0;
        public string? Origin { get; set; }
        public string? ArticleNo { get; set; }
        public string? Vendor { get; set; }
    }
}
