namespace RMPortal.WebServer.ExtendModels
{
    public class MpoHd
    {
        public int Id { get; set; }
        //采购编号
        public string? MpoNo { get; set; }
        //采购日期
        public DateTime? MpoDate { get; set; }
        //采购类型
        public string? MpoType { get; set; }
        //联系人
        public string? Attn { get; set; }
        //所属工厂抬头
        public string? Heading { get; set; }
        //货运方式
        public string? ShipMode { get; set; }
        //货运时间
        public DateTime? ShipDate { get; set; }
        public string? Status { get; set; }
        //供应商
        public string? Supplier { get; set;}
        public string? JobNo { get; set; }
        public string? Ccy { get; set; }
        public string? Terms { get; set; }
        public string? Payment { get; set; }
        public string? ShippedTo { get; set; }

    }
}
