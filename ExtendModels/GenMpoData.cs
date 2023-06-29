namespace RMPortal.WebServer.ExtendModels
{
    public class GenMpoData
    {
        public string? JobNo { get; set; }
        public string? StyleNo { get; set; }
        public string? MaterialCode { get; set; }
        public string? TempMaterial { get; set; }
        public string? ColorCode { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal QeqQty { get; set; }
        public decimal OustandingMpoQty { get; set; }   
        public decimal NetReqBal { get; set; }
        public decimal NetReqBalYDS { get; set; }
        public string? Vendor { get; set; }
        public string? ArticleNo { get; set;}
        public string? BuyUnit { get; set; }
        public string? PriceUnit { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
