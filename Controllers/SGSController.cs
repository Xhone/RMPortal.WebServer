using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Businesses;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Models.SGS;
using RMPortal.WebServer.Models.Sys;
using System.Data;

namespace RMPortal.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SGSController : ControllerBase
    {
        private readonly Dictionary<string, string> FILTERS = new Dictionary<string, string>()
        {
            {"JobNo","JobNo" },{"JobType","JobType"},{"StyleNo","StyleNo"},
            {"SuppCode","SuppCode"},{"CustCode","CustCode"},{"CustStyle","CustStyle"},
            {"Season","Season"}
        };

        private readonly Dictionary<string, string> MATHEAD_FILTERS = new Dictionary<string, string>()
        {
            {"MatCode","MatCode" },{"Type","Type"},{"SubType","SubType"},
            
        };
        /// <summary>
        /// 获取MpoType
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMpoType")]
        public async Task<ActionResult<IEnumerable<string>>> GetMpoType()
        {
            try
            {
                using (var dbContext = new SGSContext())

                {
                  

                    var result = await dbContext.Database.SqlQuery<string>($"select Field1 from MaGlobalDet where KeyNo=70151").ToListAsync();

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        /// <summary>
        /// 获取MpoHeading
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMpoHeading")]
        public async Task<ActionResult<IEnumerable<string>>> GetMpoHeading()
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    var result = await dbContext.Database.SqlQuery<string>($"Select SC_Heading from MaSCHeading").ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 获取团队
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTerms")]
        public async Task<ActionResult<IEnumerable<string>>> GetTerms()
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    var result = await dbContext.Database.SqlQuery<string>($"select Field1 as Team from MaGlobalDet where KeyNo = 70006 order by Seq").ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 获取付款方式
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPayment")]
        public async Task<ActionResult<IEnumerable<string>>> GetPayment()
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    var result = await dbContext.Database.SqlQuery<string>($"select Payment from view_HelpGlobalDetPayment Where KeyNo = 70007 order by Seq").ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCountry")]
        public async Task<ActionResult<IEnumerable<string>>> GetCountry()
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    var result = await dbContext.Database.SqlQuery<string>($"select Country from MaCountry order by Country").ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSupplier")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSupplier()
        {
            try
            {
                using(var dbContext = new SGSContext())
                {
                    FormattableString sql = $"select SuppCode, isnull(EngName,'') as EngName, isnull(ChnName,'') as ChnName, \r\nSuppStatus=case Status when 'H' then 'Hold  ' when 'X' then 'Cancel' else 'Active' end,  \r\ncase SuppType \r\nwhen 'F' then 'Factory' \r\nwhen 'S' then 'Supplier' \r\nelse 'Both' end as SuppType \r\nfrom MaSuppHd (nolock)\r\nwhere (SuppType = 'S' or SuppType = 'B') Order by SuppCode";
                    var result =await dbContext.Suppliers.FromSql(sql).ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// 获取货币
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCcy")]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCcy()
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    FormattableString sql = $"select Ccy ,BuyRate as Rate from MaCurrency";
                    var result = await dbContext.Currencys.FromSql(sql).ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// 获取surcharge
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSurType")]
        public async Task<ActionResult<IEnumerable<Surcharge>>> GetSurType()
        {
            try
            {
                using( var dbContext = new SGSContext())
                {
                    FormattableString sql = $"select Field1 as SurType, Field2 as SurDescription from MaGlobalDet where KeyNo=70038 order by KeyId";
                    var result= await dbContext.Surcharges.FromSql(sql).ToListAsync();
                    return Ok(result);

                }
            }
            catch(SqlException ex) 
            {
                
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 获取运输目的地
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetShipped")]
        public async Task<ActionResult<IEnumerable<Shipped>>> GetShipped()
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    FormattableString sql = $"select SuppCode, EngName, ChnName from MaSuppHd(nolock) where SuppType in ('F', 'B') order by SuppCode";
                    var result = await dbContext.Shippeds.FromSql(sql).ToListAsync();
                    return Ok(result);

                }
            }
            catch (SqlException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetJobOrder")]
        public async Task<ActionResult<IEnumerable<JobOrder>>> GetJobOrder()
        {
            try
            {
                using (var context = new SGSContext())
                {
                    FormattableString sql = $"SELECT top 500 a.JobNo, JobDate=convert(varchar(10),a.JobDate,111), a.JobType, a.StyleNo, a.SuppCode, a.CustCode, a.CustStyle, a.Season, JobDeliDate=convert(varchar(10),a.JobDeliDate,111) , a.SalesTeamId, b.SalesTeamCode FROM TxJobHd a (NOLOCK)  left join MaSalesTeam b on a.SalesTeamId = b.SalesTeamId Where JobType<>'D' Order By a.JobDate desc, a.JobNo, a.StyleNo";
                    var result = await context.JobOrders.FromSql(sql).ToListAsync();
                    return Ok(result);
                }

            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("GetJobOrderByFilter")]
        public async Task<ActionResult<IEnumerable<JobOrder>>> GetJobOrderByFilter(string? name, string? value1, string? value2)
        {
            try
            {
                string? n = "";
                //检测传入的name，防止依赖注入
                n = FILTERS.TryGetValue(name, out n) ? n : "JobNo";

                var v1 = string.IsNullOrEmpty(value1) ? "" : value1.ToUpper();
                var v2 = string.IsNullOrEmpty(value2) ? "" : value2.ToUpper();
                using (var context = new SGSContext())
                {


                    FormattableString sql = $"SELECT top 500 a.JobNo, JobDate=convert(varchar(10),a.JobDate,111), a.JobType, a.StyleNo, a.SuppCode, a.CustCode, a.CustStyle, a.Season, JobDeliDate=convert(varchar(10),a.JobDeliDate,111) , a.SalesTeamId, b.SalesTeamCode FROM TxJobHd a (NOLOCK)  left join MaSalesTeam b on a.SalesTeamId = b.SalesTeamId Where JobType<>'D' Order By a.JobDate desc, a.JobNo, a.StyleNo";

                    if (!string.IsNullOrEmpty(v1) && !string.IsNullOrEmpty(v2))
                    {
                        SqlParameter[] parameters = new SqlParameter[]{
                            new SqlParameter
                            {
                                ParameterName="p0",
                                Value=v1
                            },
                            new SqlParameter
                            {
                                ParameterName="p1",
                                Value=v2
                            }
                        };

                        var sql1 = $" Select Top 500 a.JobNo, JobDate=convert(varchar(10),a.JobDate,111), a.JobType, a.StyleNo, a.SuppCode, a.CustCode, a.CustStyle, a.Season, JobDeliDate=convert(varchar(10),a.JobDeliDate,111) , a.SalesTeamId, b.SalesTeamCode FROM TxJobHd a (NOLOCK)  left join MaSalesTeam b on a.SalesTeamId = b.SalesTeamId    \r\n Where (JobType<>'D'\r\n) and (a.SalesTeamId\r\n is null or a.SalesTeamId\r\n in (65,67,68,69,70,71,72,75,76,77,78,79,80)) and  {n} >= @p0 and {n} <= @p1   ORDER BY a.JobNo desc ,a.StyleNo";
                        var res = await context.JobOrders.FromSqlRaw(sql1, parameters).ToListAsync();
                        return Ok(res);
                    }
                    else if (!string.IsNullOrEmpty(v1))
                    {
                        v1 += "%";
                      
                        var p1 = new SqlParameter { ParameterName = "p0", Value = v1 };
                        var sql1 = $"Select Top 500 a.JobNo, JobDate=convert(varchar(10),a.JobDate,111), a.JobType, a.StyleNo, a.SuppCode, a.CustCode, a.CustStyle, a.Season, JobDeliDate=convert(varchar(10),a.JobDeliDate,111) , a.SalesTeamId, b.SalesTeamCode FROM TxJobHd a (NOLOCK)  left join MaSalesTeam b on a.SalesTeamId = b.SalesTeamId    \r\n Where (JobType<>'D'\r\n) and (a.SalesTeamId\r\n is null or a.SalesTeamId\r\n in (65,67,68,69,70,71,72,75,76,77,78,79,80)) and  {n} like @p0   ORDER BY a.JobNo desc ,a.StyleNo";
                        var res = await context.JobOrders.FromSqlRaw(sql1, p1).ToListAsync();
                        return Ok(res);
                    }
                    else
                    {

                        sql = $"Select Top 500 a.JobNo, JobDate=convert(varchar(10),a.JobDate,111), a.JobType, a.StyleNo, a.SuppCode, a.CustCode, a.CustStyle, a.Season, JobDeliDate=convert(varchar(10),a.JobDeliDate,111) , a.SalesTeamId, b.SalesTeamCode FROM TxJobHd a (NOLOCK)  left join MaSalesTeam b on a.SalesTeamId = b.SalesTeamId    \r\n Where (JobType<>'D'\r\n) and (a.SalesTeamId\r\n is null or a.SalesTeamId\r\n in (65,67,68,69,70,71,72,75,76,77,78,79,80))  ORDER BY a.JobNo desc ,a.StyleNo";
                    }


                    var result = await context.JobOrders.FromSql(sql).ToListAsync();
                    return Ok(result);
                }

            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GetSumPo")]
        public async Task<ActionResult<IEnumerable<PoData>>> GetSumPoData([FromBody] List<GenPOData> genPODatas)
        {
            var result = await Task.Run(() => { return GenMpoBiz.GetSumPoData(genPODatas); });
            if(result==null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// 从GenMpo的明细汇总TxMpoMatDet的数据
        /// </summary>
        /// <param name="genPODatas"></param>
        /// <returns></returns>
        [HttpPost("GetMatDetail")]
        public async Task<ActionResult<IEnumerable<MatDetailData>>> GetMatDetailData([FromBody] List<GenPOData> genPODatas)
        {
            var result = await Task.Run(() => { return GenMpoBiz.GetMatDetailData(genPODatas); });
            if(result==null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("GetMaterialData")]
        public async Task<ActionResult<IEnumerable<MaMatHead>>> GetMaterialData()
        {
            try
            {
                using (var context = new SGSContext())
                {
                    FormattableString sql = $"select top 500 MatCode,Type,SubType,Description,MeasUnit, Active from MaMatHead (nolock) where Active='Y' order by MatCode desc";
                    var result = await context.MaMatHeads.FromSql(sql).ToListAsync();
                    return Ok(result);
                }

            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("GetMaterialDataByFilter")]
        public async Task<ActionResult<IEnumerable<MaMatHead>>> GetMaterialDataByFilter(string? name, string? value1, string? value2)
        {
            try
            {
                string? n = "";
                //检测传入的name，防止依赖注入
                n = MATHEAD_FILTERS.TryGetValue(name, out n) ? n : "MatCode";

                var v1 = string.IsNullOrEmpty(value1) ? "" : value1.ToUpper();
                var v2 = string.IsNullOrEmpty(value2) ? "" : value2.ToUpper();
                using (var context = new SGSContext())
                {


                    FormattableString sql = $"select top 500 MatCode,Type,SubType,Description,MeasUnit, Active from MaMatHead (nolock) where Active='Y' order by MatCode desc";

                    if (!string.IsNullOrEmpty(v1) && !string.IsNullOrEmpty(v2))
                    {
                        SqlParameter[] parameters = new SqlParameter[]{
                            new SqlParameter
                            {
                                ParameterName="p0",
                                Value=v1
                            },
                            new SqlParameter
                            {
                                ParameterName="p1",
                                Value=v2
                            }
                        };

                        var sql1 = $"select top 500 MatCode,Type,SubType,Description,MeasUnit, Active from MaMatHead (nolock) where Active='Y' and {n} >= @p0 and {n} <= @p1 order by MatCode desc";
                        var res = await context.MaMatHeads.FromSqlRaw(sql1, parameters).ToListAsync();
                        return Ok(res);
                    }
                    else if (!string.IsNullOrEmpty(v1))
                    {
                        v1 += "%";

                        var p1 = new SqlParameter { ParameterName = "p0", Value = v1 };
                        var sql1 = $"select top 500 MatCode,Type,SubType,Description,MeasUnit, Active from MaMatHead (nolock) where Active='Y' and  {n} like @p0 order by MatCode desc";
                        var res = await context.MaMatHeads.FromSqlRaw(sql1, p1).ToListAsync();
                        return Ok(res);
                    }
                    else
                    {

                      
                    }


                    var result = await context.MaMatHeads.FromSql(sql).ToListAsync();
                    return Ok(result);
                }

            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 获取物料的明细数据
        /// </summary>
        /// <param name="matCode"></param>
        /// <returns></returns>
        [HttpGet("GetMaMatDetail")]
        public async Task<ActionResult<MaMatDetail>> GeMaMatDetail(string matCode)
        {
            var result = await Task.Run(() =>
            {
                return DTradeBiz.GetMaMatDetail(matCode);
            });

            if(result == null)
                return NotFound();
            return Ok(result[0]);
        }
    }
}
