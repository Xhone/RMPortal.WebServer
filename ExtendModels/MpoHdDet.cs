namespace RMPortal.WebServer.ExtendModels
{
    public class MpoHdDet
    {
        public int Id { get; set; }
        public string? MpoNo { get; set; }
        public string? Revision { get; set; }
        public DateTime? MpoDate { get; set; }
        public string? Heading { get; set; }
        public string? SuppCode { get; set; }
        public string? Terms
        {
            get; set;
        }
        public string? DeliAdd { get; set; }
        public DateTime? ShipDate { get; set; }
        public string? Lighting { get; set; }
        public string? Ccy { get; set; }
        public string? Attn { get; set; }
        public string? Remark { get; set; }
        public char Status { get; set; }
        public string? Payment { get; set; }
        public char SubconFlag { get; set; }
        public string? SubconType { get; set; }
        public string? JobNoStr { get; set; }
        public string? InCharge { get; set; }
        public DateTime? UDDate1 { get; set; }
        public string? UDField3 { get; set; }
        public decimal? AllowPurchase { get; set; }
        public int MpoDetId { get; set; }
        public int? Seq { get; set; }
        public string? MatCode { get; set; }
        public string? TempMat { get; set; }
        public string? ColorCode { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal? Qty { get; set; }
        public decimal? MrQty { get; set; }
        public decimal? StockQty { get; set; }
        public decimal? FirstMrQty { get; set; }
        public string? BuyUnit { get; set; }
        public decimal? Upx { get; set; }
        public string? PxUnit { get; set; }
        public decimal? Width { get; set; }
        public decimal? Weight { get; set; }
    }
}
