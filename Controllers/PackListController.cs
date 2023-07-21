using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Models.PackList;
using RMPortal.WebServer.Util;
using System.Data;
using System.Text.Json;
using WebApplication3.Dao;

namespace RMPortal.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackListController : Controller
    {
        [EnableCors("ui_policy")]
        [HttpGet]
        [Route("GetPackListExcel")]
        public bool GetPackListExcel(string id)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmm");
            //DataTable packlistDataTable = new SqlUtil().getDataTable("findPackList", "@PackNo", id, "Str_sqlcon3");
            string sqlFindPackList = $@"
                    select 							
                        t2.PoNo as PONO,				
                        t2.BatchNo as JOBNO,				
                        t6.MatCode as MATCODE,				
                        t6.ColorCode as COLORCODE,				
                        t6.Sizes as SIZE,				
                        t4.NW as NW,				
                        t4.GW as GW,				
                        t1.InvoiceNo as INVOICENO,				
                        t7.BuyUnit as UNIT,				
                        t2.CtnQty as QTYINKG,				
                        t2.Qty as QTYINYARD,				
                        t8.Lot as Lot				
                        from TxMatPackListNewHd t1				
                        left join TxMatPackListNewSum t2 on t1.PackNo=t2.PackNo				
                        left join TxMatPackListNewCtn t3 on t3.PackNo =t2.PackNo and t3.SumId=t2.SumId				
                        left join TxMatPackListNewCtnWT t4 on t4.PackNo=t3.PackNo and t4.Ctn = t3.Ctn				
                        left join TxMatPackListNewCtnMultiWT t5 on t5.PackNo=t3.PackNo				
                        left join NiMatKey t6 on t6.MatKey = t2.MatKey				
                        left join TxMpoMatDet t7 on t7.MpoNo = t2.PoNo				
                        left join TX_Order_ShipLot t8 on t8.Order_No=t2.BatchNo				
                        where t1.PackNo='{id}'
                
            ";
            DataTable packlistDataTable = new SqlUtil().getDataTable(sqlFindPackList, "TxMatPackListNewHd", "Str_sqlcon3");
            string name = "PackList" + date + ".xlsx";
            string sql = $@"
                    select distinct t2.PoNo as PONO
                        from TxMatPackListNewHd t1		
                        left join TxMatPackListNewSum t2 on t1.PackNo=t2.PackNo		where  t1.PackNo='{id}'
            ";
            DataTable poDataTable = new SqlUtil().getDataTable(sql, "TxMatPackListNewHd", "Str_sqlcon3");
            for (int i = 0; i < poDataTable.Rows.Count; i++)
            {

                string poId = poDataTable.Rows[i]["PONO"] + "";

                //DataTable poData = new SqlUtil().getDataTable("findPO", "@PoNo", poId, "Str_sqlcon3");
                string sqlPoData = $@"
                        select 			
                            t1.MpoNo as PO_ORDER_ID,   --采购订单号			
                            t1.Revision as VERSION,  --版本号			
                            t1.CreateDateTime as PO_ORDER_DATE,  --订单日期			
                            t1.SuppCode as GMT_SUPPLIER_CODE,  --供应商编码			
                            t1.Country as YARN_SUPPLIER_COUNTRY,   --国家			
                            t1.Ccy as CCY,      --货币			
                            t1.Status as STATUS_CODE,   --状态			
                            t1.ShipDate as SHIP_DATE, --发货日期			
                            t1.JobNoStr as JobNos,  --JobNos			
                            t1.UDField3 as  DLY_COUNTRY,   --ship to 工厂名称			
                            t1.Attn as GMT_SUPPLIER_CONPER,    --供应商联系人			
                            t1.CreateUser as MERCHANDISER,  --跟单员			
                            --t4.EngName,  --供应商名称			
                            t4.BuyUnit as UOM,   --单位			
                            t4.Origin as ORIGIN,   --在产地国家			
                            t4.MpoAmt as AMOUNT,    --金额			
                            t4.Destination as DEST_PORT ,  --目的地			
			
                            t3.Qty as QTY,--数量			
                            t3.UPx as PRICE, --单价			
                            t5.SuppAddr as YARN_SUPPLIER_ADDR, --供应商地址			
                            t5.FCountry as COUNTRY_ORIGIN,  --发货国家			
                            t5.TCountry as DEST_PORT_COUNTRY ,  --目的地国家			
                            t5.Terms as PAY_TERM_CODE,  --Pament Code			
                            t5.TCountry as DLY_COUNTRY, --SHIP TO工厂国家			
                            t5.Mode as SHIP_MODE,    -- Ship Mode			
                            --t6.email			
                            t7.Season as SEASON_CODE,   --Season			
                            t7.Year as SEASON_YEAR,     --Season Year			
                            t7.CustCode as CUST_CODE,  --客户编码			
                            t8.Color  as COLOR_DESC,   --颜色描述			
                            t8.ColorRemark as COLOR_REF, --颜色描述2			
                            t8.ColorRemark2 as COLOR_REF2  --颜色描述3			
                            from 			
                            TxMpoHd t1			
                            left join MaSuppHd t2 on t1.SuppCode =t2.SuppCode			
                            left join TxMpoDet t3 on  t3.MpoNo = t1.MpoNo			
                            left join TxMpoMatDet t4 on t4.MpoNo = t3.MpoNo and t4.MatCode = t3.MatCode and t4.TempMat = t3.TempMat			
                            left join TxImportLcDet t9 on t9.PoNo = t1.MpoNo 			
                            left join TxImportLcAppHd t5 on t9.ImportLcAppNo = t5.ImportLcAppNo			
                            left join UtUser t6 on t1.CreateUser = t6.UserID			
                            left join TX_Order_Header t7 on t7.Order_No = t1.MpoNo			
                            left join TX_Order_DetColor t8 on t8.Order_No = t1.MpoNo			
                            where t1.MpoNo	= '{poId}'
                ";
                DataTable poData = new SqlUtil().getDataTable(sqlPoData, "TxMpoHd", "Str_sqlcon3");
                string poname = "PO" + date + "_" + (i) + ".xlsx";
                new ExcelUtil().DataSetToExcel(poData, false, poname);
            }
            bool isPackListExport = new ExcelUtil().DataSetToExcel(packlistDataTable, false, name);
            return isPackListExport;
        }

        [EnableCors("ui_policy")]
        [HttpPost]
        [Route("SavePages")]
        public bool SavePages([FromBody] dynamic packListAllInfo)
        {
            string sqlSummary = "";
            string sqlNewIntoHeadTable = "";
            dynamic packListNewAllInfo = JsonConvert.DeserializeObject(Convert.ToString(packListAllInfo));

            string exportflag = Convert.ToString(packListNewAllInfo.exportFlag_k);
            if (exportflag.Equals("已导出"))
            {
                packListNewAllInfo.exportFlag_k = 1;
            }
            else if (exportflag.Equals("未导出"))
            {
                packListNewAllInfo.exportFlag_k = 0;
            }
            sqlNewIntoHeadTable += $@"
                 insert into TxMatPackListNewHd(
	                  PackNo
	                  ,PackDate
	                  ,FrStr
	                  ,ToStr
	                  ,ETD
	                  ,ETA
	                  ,VoyNo
                      ,MatConteact
                      ,PreparedBy
                      ,ShippingRemark
                      ,Remark
                      ,VIA
                      ,Vessel
                      ,Ccy
                      ,CcyRate
                      ,TtlCtn
                      ,TtlNW
                      ,TtlGW
                      ,SuppCode
                      ,SCHeading
                      ,FrCountry
                      ,ToCountry
                      ,Mode
                      ,Container
                      ,SealNo
                      ,BLNo
                      ,ExportFlag
                      ,FabTrim
                  )
                  values('{packListNewAllInfo.PackNo+""}',
                        '{packListNewAllInfo.PackDate + ""}',
                        '{packListNewAllInfo.inputFrom + ""}',
                        '{packListNewAllInfo.inputTo + ""}',
                        '{packListNewAllInfo.inputETD + ""}',
                        '{packListNewAllInfo.inputETA + ""}',
                        '{packListNewAllInfo.inputVoyNo + ""}',
                        '{packListNewAllInfo.inputMatContract + ""}',
                        '{packListNewAllInfo.PreparedBy + ""}',
                        '{packListNewAllInfo.textarea3_k + ""}',
                        '{packListNewAllInfo.textarea4_k + ""}',
                        '{packListNewAllInfo.inputVIA + ""}',
                        '{packListNewAllInfo.inputVessel + ""}',
                        '{packListNewAllInfo.Ccy + ""}',
                        '{packListNewAllInfo.CcyRate + ""}',
                        '{packListNewAllInfo.TtlCtn + ""}',
                        '{packListNewAllInfo.TtlNW_k + ""}',
                        '{packListNewAllInfo.TtlGW_k + ""}',
                        '{packListNewAllInfo.Factory + ""}',
                        '{packListNewAllInfo.SCHeading_k + ""}',
                        '{packListNewAllInfo.inputOrigin + ""}',
                        '{packListNewAllInfo.inputToCountry + ""}',
                        '{packListNewAllInfo.inputMode + ""}',
                        '{packListNewAllInfo.inputContainer + ""}',
                        '{packListNewAllInfo.inputSealNo + ""}',
                        '{packListNewAllInfo.inputBlNo + ""}',
                        '{packListNewAllInfo.exportFlag_k +""}',
                        '{packListNewAllInfo.fabtrim_k + ""}'
                    );
             ";

            //因为insert语句只能insert1000条，所以解决方案为超过500条就再拼接inset语句
            Array summaryDataTable = Util.Utils.getListByArray(packListNewAllInfo.SummaryTable);
            int thousands = summaryDataTable.Length / 1000;
            int count = summaryDataTable.Length - 1;
            int index = summaryDataTable.Length - 1;
            for (int i = thousands; i >= 0; i--)
            {
                string sqlSummaryIntoNewTable = "";
                sqlSummaryIntoNewTable += $@"
                    insert into TxMatPackListNewSum
                      (
	                       [PackNo]
                          ,[SumId]
                          ,[Seq]
                          ,[MatKey]
                          ,[MatCode]
                          ,[TempMat]
                          ,[Color]
                          ,[ColorCode]
                          ,[ChnColor]
                          ,[ChnColorDesc]
                          ,[Sizes]
                          ,[PoNo]
                          ,[JobNo]
                          ,[UPx]
                          ,[QtyYDS]
                          ,[QtyM]
                          ,[QtyPCS]
                          ,[QtyKG]
                          ,[PKGS]
                          ,[InvoiceNo]
                          ,[ChnMatDesc]
                          ,[DocNo]
                          ,[IssusUnit]
                          ,[MpoDescription]
                          ,[CustomDescription]
                          ,[RollNo]
                          ,[Ctn]
                      )
                      values
                 ";
                string values = "";
                count = index;
                for (int k = count; k >= i * 1000; k--)
                {
                    dynamic row = summaryDataTable.GetValue(k);
                    string ucpx = row.uc_Px == "" || row.uc_Px == null ? "0" : row.uc_Px;
                    string qtyyds = row.QtyYds == "" || row.QtyYds == null ? "0" : row.QtyYds;
                    string qtym = row.QtyM == "" || row.QtyM == null ? "0" : row.QtyM;
                    string qtypcs = row.Qtypc == "" || row.Qtypc == null ? "0" : row.Qtypc;
                    string qtykg = row.QtyKg == "" || row.QtyKg == null ? "0" : row.QtyKg;
                    string pkgs = row.pkgs == "" || row.pkgs == null ? "0" : row.pkgs;
                    values += $@"  ('{packListNewAllInfo.PackNo + ""}','{row.SumId}','{row.Seq}','{row.MatKey}','{row.Mat_Code}','{row.Temp_Mat}','{row.Color}','{row.Color_Code}','{row.ChnColor}','{row.ColorDesc}','{row.sizes}','{row.Mpo_No}','{row.Job_No}','{ucpx}','{qtyyds}','{qtym}','{qtypcs}','{qtykg}','{pkgs}','{row.Invoice_No}','{row.ChMatDesc}','{row.DocNo}','{row.Issus_Unit}','{row.MPO_Description}','{row.Custom_Description}','{row.RollNo}','{row.ctn_No}'),";
                    index = k;
                }
                index--;
                values = values.Substring(0, values.Length - 1);
                values += ";";
                sqlSummaryIntoNewTable += values;
                sqlSummary += sqlSummaryIntoNewTable;
            }

            string sqlDelete = @$"delete from TxMatPackListNewHd where PackNo='{packListNewAllInfo.PackNo}';
                           delete from TxMatPackListNewSum where PackNo='{packListNewAllInfo.PackNo}';";

            string sqlCtn = $@"select Ctn,GW,NW,CtnLength,CtnWidth,CtnHeigth,CtnQty,CBM from TxMatPackListNewCtnWT  where PackNo = '{packListNewAllInfo.PackNo}';";
            DataTable ctnData = new SqlUtil().getDataTable(sqlCtn, "TxMatPackListNewCtnWT", "Str_sqlcon3");


            //执行sql
            string sqlInsertCtn = "";
            if (ctnData.Rows.Count > 0)
            {

                foreach (DataRow dr in ctnData.Rows)
                {
                    dynamic ctn = (dr["Ctn"] + "") == "" ? "0" : dr["Ctn"];
                    dynamic nw = (dr["NW"] + "") == "" ? 0 : dr["NW"];
                    dynamic gw = (dr["GW"] + "") == "" ? 0 : dr["GW"];
                    dynamic clength = (dr["CtnLength"] + "") == "" ? 0 : dr["CtnLength"];
                    dynamic cwidth = (dr["CtnWidth"] + "") == "" ? 0 : dr["CtnWidth"];
                    dynamic cheight = (dr["CtnHeigth"] + "") == "" ? 0 : dr["CtnHeigth"];
                    dynamic cbm = (dr["CBM"] + "") == "" ? 0 : dr["CBM"];
                    dynamic ctnQty = (dr["CtnQty"] + "") == "" ? 0 : dr["CtnQty"];
                    sqlInsertCtn += $@"  insert into TxMatPackListNewCtnWT(PackNo,Ctn,NW,GW,CtnLength,CtnWidth,CtnHeight,CBM,CtnQty)
                                        values('{packListNewAllInfo.PackNo}',{ctn},{nw},{gw},{clength},{cwidth},{cheight},{cbm},{ctnQty});";
                }

            }
            string[] sqls = new string[4] { sqlDelete, sqlNewIntoHeadTable, sqlSummary, sqlInsertCtn };
            bool isSuccess = new SqlUtil().doTransaction(sqls, "Str_sqlcon4");

            return isSuccess;


        }


        [EnableCors("ui_policy")]
        [HttpGet]
        [Route("GetStkAdj")]
        public async Task<List<Summary>> GetStkAdj(string id)
        {
            List<Summary> summaryList = new List<Summary>();
            string sqlSktAdj = $@"
               select
					    t2.DetId as Seq,
                        t2.MatKey as MatKey,
                        t2.TxnNo as DocNo,
                        t2.PoNo as PoNo,
                        t2.UPx as UPx,
                        t3.MatCode as MatCode,
                        t3.TempMat as TempMat,
                        t2.BatchNo as JobNo,
                        t3.BuyUnit as Unit,
                        t4.ColorCode as ColorCode,
                        t4.Color as Color,
                        t4.ChnColor as ChnColor,
                        t4.Sizes as Sizes,
                        t2.Remark as MpoDesc
                    from TxNi_StkAdjHd t1
                    inner join TxNi_StkAdjDet t2  on   t2.TxnNo = t1.TxnNo
                    inner join TxMpoMatDet  t3    on   t2.PoNo =t3.MpoNo
                    inner join  NiMatKey t4       on   t2.MatKey=t4.MatKey
                    where t1.TxnNo ='{id}'
            ";
            DataTable markSktAdjData = new SqlUtil().getDataTable(sqlSktAdj, "TxNi_StkAdjHd", "Str_sqlcon3");
            if (markSktAdjData.Rows.Count > 0)
            {
                foreach (DataRow row in markSktAdjData.Rows)
                {
                    Summary summary = new Summary();
                    summary.Seq = row["Seq"] + "";
                    summary.MatKey = row["MatKey"] + "";
                    summary.DocNo = row["DocNo"] + "";
                    summary.Mpo_No = row["PoNo"] + "";
                    summary.uc_Px = row["UPx"] + "";
                    summary.Mat_Code = row["MatCode"] + "";
                    summary.Temp_Mat = row["TempMat"] + "";
                    summary.Job_No = row["JobNo"] + "";
                    summary.Color_Code = row["ColorCode"] + "";
                    summary.sizes = row["Sizes"] + "";
                    summary.Issus_Unit = row["Unit"] + "";
                    summary.Color = row["Color"] + "";
                    summary.ChnColor = row["ChnColor"] + "";
                    summary.MPO_Description = row["MpoDesc"] + "";
                    summaryList.Add(summary);
                }
            }

            return summaryList;
        }


        [EnableCors("ui_policy")]
        [HttpGet]
        [Route("GetMarkDN")]
        public async Task<List<Summary>> GetMarkDN(string id)
        {

            List<Summary> summaryList = new List<Summary>();
            string sqlMarkDN = $@"
                select 
                    t2.DetId as Seq,
                    t2.MatKey as MatKey,
                    t2.TxnNo as DocNo,
                    t2.PoNo as PoNo,
                    t2.UPx as UPx,
                    t3.MatCode as MatCode,
                    t3.TempMat as TempMat,
                    t2.BatchNo as JobNo,
                    t1.InvoiceNo as InvoiceNo,
                    t3.BuyUnit as Unit,
                    t4.ColorCode as ColorCode,
                    t4.Color as Color,
                    t4.ChnColor as ChnColor,
                    t4.Sizes as Sizes,
                    t2.Remark as MpoDesc
                    from TxNi_StkDnHd t1
                    inner join  TxNi_StkDnDet t2 on t1.TxnNo = t2.TxnNo
                    inner join  TxMpoMatDet t3 on t2.PoNo =t3.MpoNo
                    inner join  NiMatKey t4 on t2.MatKey=t4.MatKey
                    where t2.TxnNo ='{id}'
            ";
            DataTable markDNData = new SqlUtil().getDataTable(sqlMarkDN, "TxNi_StkDnHd", "Str_sqlcon3");
            if (markDNData.Rows.Count > 0)
            {
                foreach (DataRow row in markDNData.Rows)
                {
                    Summary summary = new Summary();
                    summary.Seq = row["Seq"] + "";
                    summary.MatKey = row["MatKey"] + "";
                    summary.DocNo = row["DocNo"] + "";
                    summary.Mpo_No = row["PoNo"] + "";
                    summary.uc_Px = row["UPx"] + "";
                    summary.Mat_Code = row["MatCode"] + "";
                    summary.Temp_Mat = row["TempMat"] + "";
                    summary.Job_No = row["JobNo"] + "";
                    summary.Color_Code = row["ColorCode"] + "";
                    summary.sizes = row["Sizes"] + "";
                    summary.Issus_Unit = row["Unit"] + "";
                    summary.Color = row["Color"] + "";
                    summary.ChnColor = row["ChnColor"] + "";
                    summary.Invoice_No = row["InvoiceNo"] + "";
                    summary.MPO_Description = row["MpoDesc"] + "";
                    summaryList.Add(summary);
                }
            }

            return summaryList;
        }

        [EnableCors("ui_policy")]
        [HttpGet]
        [Route("GetPackList")]
        public  PackListAllInfo GetPackList(string id)
        {
            string SQLstr = $@"
                select 
                    PackNo,PackDate,Ccy,CcyRate,SuppCode as Factory,
                    PreparedBy,SCHeading,TtlCtn,TtlNW,TtlGW,
                    FrStr as FromStr,
                    ToStr as ToStr,
                    ETD as ETD,
                    ETA as ETA,
                    UDField1 as VoyNo,
                    UDField2 as MatContract,
                    Vessel as Vessel,
                    Mode as Mode,
                    Container,
                    Terms as Terms,
                    BLNo as BLNo,
                    FrCountry as Origin,
                    ToCountry as ToCountry,
                    Payment as Payment,
                    SealNo as SealNo,
                    ShippingRemark as ShippingRemark,
                    Remark as Remark,
                    VIA
                    from TxMatPackListNewHd where PackNo ='{id}'
            ";
            DataTable packListData = new SqlUtil().getDataTable(SQLstr, "TxMatPackListNewHd", "Str_sqlcon3");
            PackListAllInfo packListAllInfo = new PackListAllInfo();
            if (packListData.Rows.Count > 0)
            {
                packListAllInfo.PackNo = packListData.Rows[0]["PackNo"] + "";
                if (packListData.Rows[0]["PackDate"].ToString() != "")
                {
                    packListAllInfo.PackDate = ((DateTime)packListData.Rows[0]["PackDate"]).ToString("yyyy-MM-dd");
                }


                packListAllInfo.Ccy = packListData.Rows[0]["Ccy"] + "";
                packListAllInfo.CcyRate = packListData.Rows[0]["CcyRate"] + "";
                packListAllInfo.Factory = packListData.Rows[0]["Factory"] + "";
                packListAllInfo.PreparedBy = packListData.Rows[0]["PreparedBy"] + "";
                packListAllInfo.SCHeading = packListData.Rows[0]["SCHeading"] + "";
                packListAllInfo.TtlCtn = packListData.Rows[0]["TtlCtn"] + "";
                packListAllInfo.TtlNW = packListData.Rows[0]["TtlNW"] + "";
                packListAllInfo.TtlGW = packListData.Rows[0]["TtlGW"] + "";

                packListAllInfo.FrStr = packListData.Rows[0]["FromStr"] + "";
                packListAllInfo.ToStr = packListData.Rows[0]["ToStr"] + "";

                if (packListData.Rows[0]["ETD"].ToString() != "")
                {
                    packListAllInfo.ETD = ((DateTime)packListData.Rows[0]["ETD"]).ToString("yyyy-MM-dd");
                }

                if (packListData.Rows[0]["ETA"].ToString() != "")
                {
                    packListAllInfo.ETA = ((DateTime)packListData.Rows[0]["ETA"]).ToString("yyyy-MM-dd");
                }


                packListAllInfo.VoyNo = packListData.Rows[0]["VoyNo"] + "";
                packListAllInfo.MatContract = packListData.Rows[0]["MatContract"] + "";
                packListAllInfo.Vessel = packListData.Rows[0]["Vessel"] + "";
                packListAllInfo.Container = packListData.Rows[0]["Container"] + "";
                packListAllInfo.BLNo = packListData.Rows[0]["BLNo"] + "";
                packListAllInfo.Origin = packListData.Rows[0]["Origin"] + "";
                packListAllInfo.ToCountry = packListData.Rows[0]["ToCountry"] + "";
                packListAllInfo.Payment = packListData.Rows[0]["Payment"] + "";
                packListAllInfo.SealNo = packListData.Rows[0]["SealNo"] + "";
                packListAllInfo.ShippingRemark = packListData.Rows[0]["ShippingRemark"] + "";
                packListAllInfo.Remark = packListData.Rows[0]["Remark"] + "";
                packListAllInfo.VIA = packListData.Rows[0]["VIA"] + "";
                packListAllInfo.Mode = packListData.Rows[0]["Mode"] + "";
                string SqlStr2 = $@"                   
		                select distinct
				                t1.Seq as Seq,
                                t1.SumId as SumId,
                                t1.MatKey as MatKey,
				                t1.DocNo as DocNo,
				                t1.PoNo as PoNo,
				                t1.UPx as UPx,
				                t2.MatCode as MatCode,
				                t2.TempMat as TempMat,
				                t1.Ctn as Ctn,
				                t1.BatchNo as JobNo,
				                t3.InvoiceNo as InvoiceNo,
				                t2.BuyUnit as Unit,
				                --t4.Lot as Lot,
				                t5.ColorCode as ColorCode,
                                t5.Color as Color,
                                t5.ChnColor as ChnColor,
				                t5.Sizes as Sizes,
                                t1.Remark1 as CustomDesc,
                                t1.Description as MpoDesc
							                from TxMatPackListNewSum t1
                                            inner join TxMpoMatDet t2 on  t1.PoNo = t2.MpoNo
							                inner join TxMatPackListNewHd t3 on t1.PackNo =t3.PackNo
							                inner join TX_Order_ShipLot t4 on t4.Order_No=t1.BatchNo
							                inner join NiMatKey t5 on t5.MatKey = t1.MatKey
							                where t1.PackNo='{id}' Order By Seq asc
                ";
                List<Summary> summaryList = new List<Summary>();
                DataTable packListSummaryData = new SqlUtil().getDataTable(SqlStr2, "TxMatPackListNewSum", "Str_sqlcon3");
                if (packListSummaryData.Rows.Count > 0)
                {
                    foreach (DataRow row in packListSummaryData.Rows)
                    {
                        Summary sum = new Summary();
                        sum.Seq = row["Seq"] + "";
                        sum.SumId = row["SumId"] + "";
                        sum.MatKey = row["MatKey"] + "";
                        sum.DocNo = row["DocNo"] + "";
                        sum.Mpo_No = row["PoNo"] + "";
                        sum.uc_Px = row["UPx"] + "";
                        sum.Mat_Code = row["MatCode"] + "";
                        sum.Temp_Mat = row["TempMat"] + "";
                        sum.ctn_No = row["Ctn"] + "";
                        sum.Job_No = row["JobNo"] + "";
                        sum.Color_Code = row["ColorCode"] + "";
                        //sum.Lot = row["Lot"] + "";
                        sum.sizes = row["Sizes"] + "";
                        sum.Issus_Unit = row["Unit"] + "";
                        sum.Color = row["Color"] + "";
                        sum.ChnColor = row["ChnColor"] + "";
                        sum.Invoice_No = row["InvoiceNo"] + "";
                        sum.MPO_Description = row["MpoDesc"] + "";
                        sum.Custom_Description = row["CustomDesc"] + "";
                        if ((row["Ctn"] + "") != "")
                        {
                            string sqlCtn = $@"select Ctn,GW,NW,CtnLength,CtnWidth,CtnHeigth,CtnQty,CBM from TxMatPackListNewCtnWT  where PackNo = '{packListAllInfo.PackNo}' and Ctn = '{sum.ctn_No}'";
                            DataTable ctnData = new SqlUtil().getDataTable(sqlCtn, "TxMatPackListNewCtnWT", "Str_sqlcon3");
                            List<CtnInfo> ctnInfoLists = new List<CtnInfo>();
                            if (ctnData.Rows.Count > 0)
                            {
                                foreach (DataRow dr in ctnData.Rows)
                                {
                                    CtnInfo ctnInfo = new CtnInfo();
                                    ctnInfo.Ctn = dr["Ctn"] + "";
                                    ctnInfo.GW = dr["GW"] + "";
                                    ctnInfo.NW = dr["NW"] + "";
                                    ctnInfo.Length = dr["CtnLength"] + "";
                                    ctnInfo.Width = dr["CtnWidth"] + "";
                                    ctnInfo.Height = dr["CtnHeigth"] + "";
                                    ctnInfo.Ctn_Qty = dr["CtnQty"] + "";
                                    ctnInfo.CBM = dr["CBM"] + "";
                                    ctnInfoLists.Add(ctnInfo);
                                }
                            }
                            sum.ctnInfoList = ctnInfoLists;
                        }


                        summaryList.Add(sum);

                    }
                }
                packListAllInfo.summaryList = summaryList;
            }

            return packListAllInfo;

        }


        [EnableCors("ui_policy")]
        [HttpPost]
        [Route("selectPackList")]
        public List<PackListAllInfo> SelectPackList()
        {
            List<PackListAllInfo> list = new List<PackListAllInfo>();
            string sqlpackList = $@"select PackNo,PackDate from TxMatPackListNewHd";
            DataTable packlistData = new SqlUtil().getDataTable(sqlpackList, "TxMatPackListNewHd", "Str_sqlcon3");
            if (packlistData.Rows.Count > 0)
            {
                foreach (DataRow row in packlistData.Rows)
                {
                    PackListAllInfo packListAllInfo = new PackListAllInfo();

                    packListAllInfo.PackNo = row["PackNo"] + "";
                    packListAllInfo.PackDate = row["PackDate"] + "";

                    list.Add(packListAllInfo);
                }
            }
            return list;
        }

        [EnableCors("ui_policy")]
        [HttpPost]
        [Route("SelectSomePackList")]
        public List<PackListAllInfo> SelectSomePackList([FromBody] dynamic obj)
        {
            List<PackListAllInfo> list = new List<PackListAllInfo>();
            string str = obj.ToString();
            string sqlpackList = $@"select PackNo,PackDate from TxMatPackListNewHd where 1=1  ";
            System.Array array = str.Split("\r\n");

            string strName = array.GetValue(1).ToString();
            strName = strName.Substring(strName.LastIndexOf(":") + 1);
            strName = Util.Utils.getRightStr(strName);
            string strValue = array.GetValue(2).ToString();
            strValue = strValue.Substring(strValue.LastIndexOf(":") + 1);
            strValue = Util.Utils.getRightStr(strValue);
            sqlpackList += @$" and {strName} = '{strValue}'";
            DataTable packlistData = new SqlUtil().getDataTable(sqlpackList, "TxMatPackListNewHd", "Str_sqlcon3");
            if (packlistData.Rows.Count > 0)
            {
                foreach (DataRow row in packlistData.Rows)
                {
                    PackListAllInfo packListAllInfo = new PackListAllInfo();

                    packListAllInfo.PackNo = row["PackNo"] + "";
                    packListAllInfo.PackDate = row["PackDate"] + "";

                    list.Add(packListAllInfo);
                }
            }


            return list;
        }


        [EnableCors("ui_policy")]
        [HttpGet]
        [Route("GetPOExcel")]
        public async Task<List<POInfo>> GetPOExcel(string id)
        {
            //DataTable poDataTable = new SqlUtil().getDataTable("findPO", "@PoNo", id, "Str_sqlcon3");
            string sqlPoData = $@"
                    select 			
                        t1.MpoNo as PO_ORDER_ID,   --采购订单号			
                        t1.Revision as VERSION,  --版本号			
                        t1.CreateDateTime as PO_ORDER_DATE,  --订单日期			
                        t1.SuppCode as GMT_SUPPLIER_CODE,  --供应商编码			
                        t1.Country as YARN_SUPPLIER_COUNTRY,   --国家			
                        t1.Ccy as CCY,      --货币			
                        t1.Status as STATUS_CODE,   --状态			
                        t1.ShipDate as SHIP_DATE, --发货日期			
                        t1.JobNoStr as JobNos,  --JobNos			
                        t1.UDField3 as  DLY_COUNTRY,   --ship to 工厂名称			
                        t1.Attn as GMT_SUPPLIER_CONPER,    --供应商联系人			
                        t1.CreateUser as MERCHANDISER,  --跟单员			
                        --t4.EngName,  --供应商名称			
                        t4.BuyUnit as UOM,   --单位			
                        t4.Origin as ORIGIN,   --在产地国家			
                        t4.MpoAmt as AMOUNT,    --金额			
                        t4.Destination as DEST_PORT ,  --目的地			
			
                        t3.Qty as QTY,--数量			
                        t3.UPx as PRICE, --单价			
                        t5.SuppAddr as YARN_SUPPLIER_ADDR, --供应商地址			
                        t5.FCountry as COUNTRY_ORIGIN,  --发货国家			
                        t5.TCountry as DEST_PORT_COUNTRY ,  --目的地国家			
                        t5.Terms as PAY_TERM_CODE,  --Pament Code			
                        t5.TCountry as DLY_COUNTRY, --SHIP TO工厂国家			
                        t5.Mode as SHIP_MODE,    -- Ship Mode			
                        --t6.email			
                        t7.Season as SEASON_CODE,   --Season			
                        t7.Year as SEASON_YEAR,     --Season Year			
                        t7.CustCode as CUST_CODE,  --客户编码			
                        t8.Color  as COLOR_DESC,   --颜色描述			
                        t8.ColorRemark as COLOR_REF, --颜色描述2			
                        t8.ColorRemark2 as COLOR_REF2  --颜色描述3			
                        from 			
                        TxMpoHd t1			
                        left join MaSuppHd t2 on t1.SuppCode =t2.SuppCode			
                        left join TxMpoDet t3 on  t3.MpoNo = t1.MpoNo			
                        left join TxMpoMatDet t4 on t4.MpoNo = t3.MpoNo and t4.MatCode = t3.MatCode and t4.TempMat = t3.TempMat			
                        left join TxImportLcDet t9 on t9.PoNo = t1.MpoNo 			
                        left join TxImportLcAppHd t5 on t9.ImportLcAppNo = t5.ImportLcAppNo			
                        left join UtUser t6 on t1.CreateUser = t6.UserID			
                        left join TX_Order_Header t7 on t7.Order_No = t1.MpoNo			
                        left join TX_Order_DetColor t8 on t8.Order_No = t1.MpoNo			
                        where t1.MpoNo	= '{id}'
            ";
            DataTable poDataTable = new SqlUtil().getDataTable(sqlPoData, "TxMpoHd", "Str_sqlcon3");
            List<POInfo> JsonsList = new List<POInfo>();
            for (int i = 0; i < poDataTable.Rows.Count; i++)
            {
                POInfo poInfoJson = new POInfo();
                poInfoJson.POORDERID = poDataTable.Rows[i]["PO_ORDER_ID"] + "";
                poInfoJson.VERSION = Convert.ToInt32((poDataTable.Rows[i]["VERSION"].ToString() == "") ? -1 : poDataTable.Rows[i]["VERSION"]);
                poInfoJson.POORDERDATE = ((DateTime)poDataTable.Rows[i]["PO_ORDER_DATE"]).ToString("yyyyMMdd"); ;
                poInfoJson.GMTSUPPLIERCODE = poDataTable.Rows[i]["GMT_SUPPLIER_CODE"] + "";
                poInfoJson.YARNSUPPLIERCOUNTRY = poDataTable.Rows[i]["YARN_SUPPLIER_COUNTRY"] + "";
                poInfoJson.CCY = poDataTable.Rows[i]["CCY"] + "";
                poInfoJson.STATUSCODE = poDataTable.Rows[i]["STATUS_CODE"] + "";
                poInfoJson.SHIPDATE = ((DateTime)poDataTable.Rows[i]["SHIP_DATE"]).ToString("yyyyMMdd");
                poInfoJson.JOBNOS = poDataTable.Rows[i]["JOBNOS"] + "";
                poInfoJson.DLYCOUNTRY = poDataTable.Rows[i]["DLY_COUNTRY"] + "";
                poInfoJson.GMTSUPPLIERCONPER = poDataTable.Rows[i]["GMT_SUPPLIER_CONPER"] + "";
                poInfoJson.MERCHANDISER = poDataTable.Rows[i]["MERCHANDISER"] + "";
                poInfoJson.UOM = poDataTable.Rows[i]["UOM"] + "";
                poInfoJson.ORIGIN = poDataTable.Rows[i]["ORIGIN"] + "";
                poInfoJson.AMOUNT = poDataTable.Rows[i]["AMOUNT"] + "";
                poInfoJson.DESTPORT = poDataTable.Rows[i]["DEST_PORT"] + "";
                poInfoJson.QTY = poDataTable.Rows[i]["QTY"] + "";
                poInfoJson.PRICE = poDataTable.Rows[i]["PRICE"] + "";
                poInfoJson.YARNSUPPLIERADDR = poDataTable.Rows[i]["YARN_SUPPLIER_ADDR"] + "";
                poInfoJson.COUNTRYORIGIN = poDataTable.Rows[i]["COUNTRY_ORIGIN"] + "";
                poInfoJson.DESTPORTCOUNTRY = poDataTable.Rows[i]["DEST_PORT_COUNTRY"] + "";
                poInfoJson.PAYTERMCODE = poDataTable.Rows[i]["PAY_TERM_CODE"] + "";
                poInfoJson.DLYCOUNTRY = poDataTable.Rows[i]["DLY_COUNTRY"] + "";
                poInfoJson.SHIPMODE = poDataTable.Rows[i]["SHIP_MODE"] + "";
                poInfoJson.SEASONCODE = poDataTable.Rows[i]["SEASON_CODE"] + "";
                poInfoJson.SEASONYEAR = poDataTable.Rows[i]["SEASON_YEAR"] + "";
                poInfoJson.CUSTCODE = poDataTable.Rows[i]["CUST_CODE"] + "";
                poInfoJson.COLORDESC = poDataTable.Rows[i]["COLOR_DESC"] + "";
                poInfoJson.COLORREF = poDataTable.Rows[i]["COLOR_REF"] + "";
                poInfoJson.COLORREF2 = poDataTable.Rows[i]["COLOR_REF2"] + "";
                JsonsList.Add(poInfoJson);
            }

            return JsonsList;
        }

        [EnableCors("ui_policy")]
        [HttpPost]
        [Route("uploadFTP")]
        public bool uploadFTP([FromBody] dynamic obj)
        {
            string serverIP = "ftp://10.0.3.222/";
            string str = obj.ToString();
            System.Array array = str.Split("\r\n");
            bool isUpload = false;
            for (int i =0; i < array.Length; i++)
            {
                string fileName = Util.Utils.getRightStr(array.GetValue(i).ToString());
                string path = "";
                path = Util.Utils.getNumber(fileName);
                isUpload = Utils.FtpUtils.UploadToFtp(serverIP, "", "", @$"D:/Excel/{path}/{fileName}", @$"/{fileName}");
                if (!isUpload)
                {
                    break;
                }
            }
            return isUpload;

        }
    }
}
