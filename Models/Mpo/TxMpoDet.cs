namespace RMPortal.WebServer.Models.Mpo
{
    public class TxMpoDet
    {
        public int Id { get; set; }
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
        public TxMpoHd? TxMpoHd { get; set; }

    }
}
