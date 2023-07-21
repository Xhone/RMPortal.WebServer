namespace RMPortal.WebServer.Models.PackList
{
    public class Summary
    {
        public int id { get; set; }
        public string MatKey { get; set; }
        public string SumId { get; set; }
        public string Seq { get; set; }
        public string DocNo { get; set; }
        public string Mpo_No { get; set; }
        public string Mat_Code { get; set; }
        public string Temp_Mat { get; set; }
        public string Color_Code { get; set; }
        public string Color { get; set; }
        public string ChnColor { get; set; }
        public string ColorDesc { get; set; }
        public string sizes { get; set; }
        public string Job_No { get; set; }
        public string uc_Px { get; set; }
        public string ctn_No { get; set; }
        //public string Lot { get;set; }
        public string Invoice_No { get; set; }
        public string Issus_Unit { get; set; }
        public string MPO_Description { get; set; }
        public string Custom_Description { get; set; }

        public string QtyYds { get; set; }
        public string QtyM { get; set; }
        public string Qtypc { get; set; }
        public string QtyKg { get; set; }
        public string RollNo { get; set; }

        public string pkgs { get; set; }
        public string ChMatDesc { get; set; }
        public List<CtnInfo> ctnInfoList { get; set; }
    }
}
