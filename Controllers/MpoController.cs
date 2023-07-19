using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RMPortal.WebServer.Businesses;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Helpers;
using RMPortal.WebServer.Models.Mpo;

namespace RMPortal.WebServer.Controllers
{
    //[EnableCors("ui_policy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MpoController : ControllerBase
    {
        private readonly RMPortalContext _context;

        private readonly Secrets _secrets;

        public MpoController(RMPortalContext context,IOptions<Secrets> options)
        {
            _context = context;
            _secrets = options.Value;
        }

        // GET: api/Mpo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TxMpoHd>>> GetTxMpoHds()
        {
          
          if (_context.TxMpoHds == null)
          {
              return NotFound();
          }
          var result= await _context.TxMpoHds.ToListAsync();
            
          return Ok(result);
        }


        [HttpGet("GetMpoHd")]
        public async Task<ActionResult<IEnumerable<MpoHd>>> GetMpoHd(string? mpo,string? mpoType,string? supplier,string? status,string? jobNo,DateTime? start,DateTime? end)
        {

            var result = from hd in _context.TxMpoHds
                         where string.IsNullOrWhiteSpace(mpo) ? true : hd.MpoNo.Contains(mpo)
                         where start.HasValue ? hd.MpoDate >= start : true
                         where end.HasValue ? hd.MpoDate <= end : true
                         where string.IsNullOrWhiteSpace(mpoType)?true:hd.MpoType.Contains(mpoType)
                         where string.IsNullOrWhiteSpace(supplier)?true:hd.SuppCode.Contains(supplier)
                         where string.IsNullOrWhiteSpace(jobNo)?true:hd.JobNoStr.Contains(jobNo)
                         where string.IsNullOrWhiteSpace(status) ? true : hd.Status.Equals(status[0])
                         orderby hd.MpoDate descending, hd.Heading
                         select new MpoHd
                         {
                             Id = hd.Id,
                             MpoNo = hd.MpoNo,
                             MpoDate = hd.MpoDate,
                             MpoType = hd.MpoType,
                             Attn = hd.Attn,
                             Heading = hd.Heading,
                             ShipMode = hd.ShipMode,
                             ShipDate = hd.ShipDate,
                             Status = GenMpoBiz.GetStatus(hd.Status),
                             Supplier = hd.SuppCode,
                             JobNo = hd.JobNoStr,
                             Ccy = hd.Ccy,
                             Terms = hd.Terms,
                             Payment = hd.Payment,
                             ShippedTo = hd.ShippedTo,


                         };
            if (result == null)
            {
                return NotFound();
            }
            return await result.ToListAsync();
            

            

        }
        // GET: api/Mpo/GetMpo?id=1
        [HttpGet("GetMpo")]
        public async Task<ActionResult<TxMpoHd>> GetMpo(int id)
        {
          if (_context.TxMpoHds == null)
          {
              return NotFound();
          }

            
            var result = await _context.TxMpoHds
                .Include(det=>det.TxMpoDets)
                .ThenInclude(mr=>mr.TxMpoDetMrs)
                .Include(matDet=>matDet.TxMpoMatDets)
                
                .Include(sur=>sur.TxMpoSurcharges)
                .AsNoTracking()
                .FirstOrDefaultAsync(hd=>hd.Id==id);

            #region linq
            //result = await (from hd in _context.TxMpoHds
            //             join det in _context.TxMpoDets on hd.MpoNo equals det.MpoNo into hd_det
            //             where hd.Id.Equals(id)
            //             from hd_detl in hd_det.DefaultIfEmpty()
            //             select new TxMpoHd
            //             {

            //                 Id = hd.Id,
            //                 MpoNo = hd.MpoNo,
            //                 Revision = hd.Revision,
            //                 MpoDate = hd.MpoDate,
            //                 Heading = hd.Heading,
            //                 SuppCode = hd.SuppCode,
            //                 Terms = hd.Terms,
            //                 DeliAdd = hd.DeliAdd,
            //                 ShipDate = hd.ShipDate,
            //                 Lighting = hd.Lighting,
            //                 Ccy = hd.Ccy,
            //                 Attn = hd.Attn,
            //                 Remark = hd.Remark,
            //                 Status = hd.Status,
            //                 Payment = hd.Payment,
            //                 SubconFlag = hd.SubconFlag,
            //                 SubconType = hd.SubconType,
            //                 JobNoStr = hd.JobNoStr,
            //                 InCharge = hd.InCharge,
            //                 RevisedDate = hd.RevisedDate,
            //                 ShippedTo = hd.ShippedTo,
            //                 AllowPurchase = hd.AllowPurchase,
            //                 TxMpoDets = hd_det.ToList(),
            //             }).FirstOrDefaultAsync();

            #endregion
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);



        }
        [HttpGet("GetmpoView")]
        public async Task<ActionResult<IEnumerable<GenPOData>>> GetMpoView(string jobNo)
        {
            List<GenPOData>? r = new List<GenPOData>();
            if (jobNo.Contains(','))
            {
                var jobNos = jobNo.Split(',');
                r=await Task.Run(() => { return GenMpoBiz.GetPoData(jobNos); });
                if(r==null)
                    return NotFound();
               
            }
            else
            {
                r = await Task.Run(() => { return GenMpoBiz.GetPoData(jobNo); });
                if (r == null)
                {
                    return NotFound();
                }
                
            }
           
            return Ok(r);
           
        }

        // PUT: api/Mpo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTxMpoHd(int id, TxMpoHd txMpoHd)
        {
            if (id != txMpoHd.Id)
            {
                return BadRequest();
            }

            _context.Entry(txMpoHd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TxMpoHdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("PutMpo")]
        public async Task<IActionResult> PutTxMpoHd([FromBody]TxMpoHd txMpoHd)
        {
            if (0== txMpoHd.Id)
            {
                return BadRequest();
            }

            _context.Entry(txMpoHd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TxMpoHdExists(txMpoHd.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Response.StatusCode);
        }

        // POST: api/Mpo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Save")]
        public async Task<ActionResult> PostTxMpoHd([FromBody]TxMpoHd txMpoHd)
        {
          
          if (_context.TxMpoHds == null)
          {
              return Problem("Entity set 'RMPortalContext.TxMpoHds'  is null.");
          }

            if (txMpoHd.TxMpoDets.Count > 0 && txMpoHd.TxMpoMatDets.Count == 0)
            {
               
                txMpoHd.TxMpoMatDets = (from s in txMpoHd.TxMpoDets
                                    group s by new
                                    {
                                        s.MatCode,
                                        s.TempMat,
                                        s.BuyUnit,
                                        s.BuyUnitFactor,
                                        s.PxUnit,
                                        s.PxUnitFactor,
                                        s.Width,
                                        s.Weight,
                                        s.MatDesc
                                    } into g
                                    select new TxMpoMatDet
                                    {
                                        MpoMatDetId = 0,
                                        MpoNo = txMpoHd.MpoNo,
                                        MatCode = g.Key.MatCode,
                                        TempMat = g.Key.TempMat,
                                        MpoAmount = g.Sum(s => s.Upx / (s.PxUnitFactor ?? 1) * s.Qty),
                                        BuyUnit = g.Key.BuyUnit,
                                        BuyUnitFactor = g.Key.BuyUnitFactor,
                                        PriceUnit = g.Key.PxUnit,
                                        PriceUnitFactor = g.Key.PxUnitFactor,
                                        Width = g.Key.Width,
                                        Weight = g.Key.Weight,
                                        MatDesc = g.Key.MatDesc,
                                        Remark = "",
                                        Origin = "",


                                        TxMpoHd = null,
                                    }).ToList();
            }
            _context.TxMpoHds.Add(txMpoHd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {

            }

            return Ok(Response.StatusCode);
            //return CreatedAtAction("GetTxMpoHd", new { id = txMpoHd.Id }, txMpoHd);
        }

        // DELETE: api/Mpo/5
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteTxMpoHd(int id)
        {
            if (_context.TxMpoHds == null)
            {
                return NotFound();
            }
            var txMpoHd = await _context.TxMpoHds
                .Include(det=>det.TxMpoDets)
                .ThenInclude(mr=>mr.TxMpoDetMrs)
                .Include(mat=>mat.TxMpoMatDets)
                .Include(sur=>sur.TxMpoSurcharges)
                .FirstAsync(hd=>hd.Id==id);
            if (txMpoHd == null)
            {
                return NotFound();
            }
            
            _context.TxMpoHds.Remove(txMpoHd);
            await _context.SaveChangesAsync();

            return Ok(Response.StatusCode);
        }

        private bool TxMpoHdExists(int id)
        {
            return (_context.TxMpoHds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
