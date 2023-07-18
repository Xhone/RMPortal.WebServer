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
        public decimal? Qty { get; set; }
        public decimal? MrQty { get; set;}
        public decimal? StockQty { get; set;}
        public decimal? FirstMrQty { get;set;}
        public string? BuyUnit { get; set; }
     
        public decimal? Upx { get;set; }
        public string? PxUnit { get; set; }
        public decimal? Width { get;set; }
        public decimal? Weight { get; set; }
        public string? WidthUnit { get; set; }
        public decimal? BuyUnitFactor { get; set; }
        public decimal? PxUnitFactor { get;set; }
        public string? MatDesc { get; set; }
        public string? VendorNo { get;set; }
        public string? VendorColor { get; set; }
        public string? Origin { get; set; }
        public string? Remark { get; set; }
        public virtual TxMpoHd? TxMpoHd { get; set; }

        public ICollection<TxMpoDetMr> TxMpoDetMrs{ get; set; }=new List<TxMpoDetMr>();

    }
}
