using RMPortal.WebServer.Models.PackList;

namespace WebApplication3.Dao
{
    public class PackListAllInfo
    {

        public int Id { get; set; }
        public string PackNo { get; set; }
        public string PackDate { get; set; }
        public string Ccy { get; set; }
        public string CcyRate { get; set; }
        public string Factory { get; set; }
        public string PreparedBy { get; set; }
        public string SCHeading { get; set; }
        public string TtlCtn { get; set; }
        public string TtlNW { get; set; }
        public string TtlGW { get; set; }
        public string FrStr { get;set; }
        public string ToStr { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string VoyNo { get; set; }
        public string MatContract { get; set; }
        public string Vessel { get; set; }
        public string Mode { get; set; }
        public string Container { get; set; }
        public string Terms { get; set; }
        public string BLNo { get; set; }
        public string Origin { get; set; }
        public string ToCountry { get; set; }
        public string Payment { get; set; }
        public string SealNo { get; set; }
        public string ShippingRemark { get; set; }
        public string Remark { get; set; }
        public string VIA { get;set; }
        public List<Summary> summaryList { get; set; }

    }
}
