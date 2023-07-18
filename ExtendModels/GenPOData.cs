namespace RMPortal.WebServer.ExtendModels
{
    public class GenPOData
    {
        public string? JobNo { get; set; }
        public string? StyleNo { get; set; }
        public string? MatCode { get; set; }
        public string? TempMat { get; set; }
        public string? ColorCode { get; set; }
        public string? Color { get; set; }


        public string? Sizes { get; set; }
        public string? Ccy { get; set; }
        public decimal? Rate { get; set; }
        //price
        public decimal? UPx { get; set; }
        public string? MeasUnit { get; set; }
        public string? BuyUnit { get; set; }
        public decimal? BuyUnitFactor { get; set; }

        public string? PxUnit { get; set; }
        public decimal? PxUnitFactor { get; set; }
        public string? IssueUnit { get; set; }
        public decimal? IssueUnitFactor { get; set; }

        public string? Description { get; set; }
        public decimal? Width1 { get; set; }
        public decimal? Weight { get; set; }
        public string? MatDesc { get; set; }
        public string? SCHeading { get; set; }
        public string? WidthUnit { get; set; }
        //ReqQty
        public decimal? MrReqQty_B { get; set; }
        //Oustanding MPO Qty
        public decimal? MrOutsMpoQty_B { get; set; }
        //Net Req Bal
        public decimal? MrNetBalQty_B { get; set; }
        //Net Req Bal YDS
        public decimal? NewNetBal { get; set; }
        //vendor
        public string? SuppCode { get; set; }
        public string? SuppName { get; set; }   

        public string? ArticleNo { get; set; }
       
       
       

    }

    public class MrData
    {
        public string? MrNo { get; set; }
        //public string? MatCode { get; set; }
        public decimal? Qty { get; set; }
    }
    public class PoData
    {
        public string? MatCode { get; set; }
        public string? TempMat { get; set; }
        public string? ColorCode { get; set; }
        public string? Color { get; set; }
        public string? Sizes { get; set; }
        public decimal? MrReqQty_B { get; set; }
        public string? BuyUnit { get; set; }
        public decimal? UPx { get; set; }
        public string? PxUnit { get; set; }
        public decimal? Width1 { get; set; }
        public decimal? Weight { get; set; }
        public string? WidthUnit { get; set; }
        public decimal? BuyUnitFactor { get; set; }
        public decimal? PxUnitFactor { get; set; }
        public string? MatDesc { get; set; }
        public ICollection<MrData>? MrDatas { get; set; }= new List<MrData>();
    }

    public class MatDetailData
    {
        public string? MatCode { get; set; }
        public string? TempMat { get; set; }
        public string? BuyUnit { get; set; }
        public decimal? BuyUnitFactor { get; set; }
        public string? PriceUnit { get; set; }
        public decimal? PriceUnitFactor { get; set; }
        public decimal? Width { get; set; }
        public decimal? Weight { get; set;}
        public string? MatDesc { get; set; }
        public decimal? MpoAmount { get; set;}
    }
    public class MpoView
    {
        public string? JobNo { get; set; }
        public string? StyleNo { get; set; }
        public string? MatCode { get; set; }
        public string? TempMat { get; set; }
        public string? ColorCode { get; set; }
        public string? Color { get; set; }
       

        public string? Sizes { get; set; }
        //ReqQty
        public decimal? MrReqQty_B { get; set; }
        //Oustanding MPO Qty
        public decimal? MrOutsMpoQty_B { get; set; }    
        //Net Req Bal
        public decimal? MrNetBalQty_B { get; set; }
        //Net Req Bal YDS
        public decimal? NewNetBal { get; set; }
        //vendor
        public string? SuppCode { get; set; }
        public string? ArticleNo { get; set; }
        public string? BuyUnit { get; set; }
        public string? PxUnit { get; set; }
        //price
        public decimal? UPx { get; set; }
        public string? Description { get;set; }
    }
}
