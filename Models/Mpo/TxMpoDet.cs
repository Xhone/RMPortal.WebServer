using Microsoft.EntityFrameworkCore;

namespace RMPortal.WebServer.Models.Mpo
{
    [PrimaryKey(nameof(MpoDetId))]
    public class TxMpoDet
    {
        
        public int MpoDetId { get; set; }
        public string? MpoNo { get; set; }
        public int? Seq { get; set; }   
        public string? MatCode { get; set; }
        public string? TempMat { get; set; }
        public string? ColorCode { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal Qty { get; set; }
        public decimal MrQty { get; set;}
        public decimal StockQty { get; set;}
        public decimal FirstMrQty { get;set;}
        public string? BuyUnit { get; set; }
       // public int? TxMpoHdId { get; set; }=0;
        public decimal Upx { get;set; }
        public string? PxUnit { get; set; }
        public decimal Width { get;set; }
        public decimal Weight { get; set; }
        public virtual TxMpoHd? TxMpoHd { get; set; }

    }
}
