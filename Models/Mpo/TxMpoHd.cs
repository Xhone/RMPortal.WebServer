using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace RMPortal.WebServer.Models.Mpo
{
    public class TxMpoHd
    {
        public TxMpoHd() {
            
        }
        public int Id { get; set; }
        //采购编号
        public string? MpoNo { get; set; }
        public string? MpoType { get; set; }
        //vat% 税率
        public string? Revision { get; set; }
        //采购日期
        public DateTime? MpoDate { get; set; }
        //工厂抬头
        public string? Heading { get; set; }
        //供应商
        [AllowNull]
        public string? SuppCode { get; set; }
        //团队
        public string? Terms
        {
            get;set;
        }
        //送货地址
        public string? DeliAdd { get; set; }
        //货运日期
        public DateTime? ShipDate { get; set; }
        //运送方式
        public string? ShipMode { get; set; }
        //["ETD","ETA","CLOSING"]
        public string? Lighting { get; set; }
        //["USD","RMB","HKD"]
        public string? Ccy { get; set; }
        //联络人
        public string? Attn { get; set; }
        public string? Remark { get; set; }
        //["OPEN","SUBMIT"]
        public char Status { get; set; }
        //付款方法
        public string? Payment { get; set; }
        //Bulk Purchase
        public bool SubconFlag { get; set; }
        //Request By
        public string? SubconType { get; set; }
        //生产单号
        public string? JobNoStr { get; set; }
        //创建人
        public string? InCharge { get; set; }
        //Revised Date
        public DateTime? RevisedDate { get; set; }
        //送货至which factory
        public string? ShippedTo { get; set; }
        //允许超收货
        public decimal AllowPurchase { get; set; }
        public char OverQtyType { get; set; }
        public ICollection<TxMpoDet>? TxMpoDets { get; set; }=new List<TxMpoDet>();
        public ICollection<TxMpoMatDet>? TxMpoMatDets { get; set; } =new List<TxMpoMatDet>();
        public ICollection<TxMpoSurcharge>? TxMpoSurcharges { get; set; } =new List<TxMpoSurcharge>();
    }
}
