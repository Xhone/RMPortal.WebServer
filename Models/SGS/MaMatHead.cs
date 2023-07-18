namespace RMPortal.WebServer.Models.SGS
{
    public class MaMatHead
    {
        public string? MatCode { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public string? Description { get; set; }
        //public string? ChnDescription { get; set; }

        public string? MeasUnit { get; set; }
        public char? Active { get; set; }
    }

    public class MaMatDetail
    {
        public string? MatCode { get; set; }
        public string? Description { get; set; }      
        public decimal? Width1 { get; set; }
        public string? MeasUnit { get; set; }
        public decimal? Weight { get; set; }
        public string? SuppCode { get; set; }
        public string? MatColor { get; set; }
        public string? ColorCode { get; set; }
        public string? Ccy { get; set; }
        public decimal? Rate { get; set; }
        public decimal? UPx { get; set; }
        public string? BuyUnit { get; set; }
        public decimal? BuyUnitFactor { get; set; }
        public string? PxUnit { get; set; } 
        public decimal? PxUnitFactor { get;set; }
        public string? WidthUnit { get; set; }

    }
}
