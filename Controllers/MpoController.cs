using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<MpoHd>>> GetMpoHd(string? mpo,DateTime start,DateTime end)
        {
           
            var result = from hd in _context.TxMpoHds
                         where string.IsNullOrWhiteSpace(mpo) ? true:hd.MpoNo.Contains(mpo)
                         where hd.MpoDate >= start && hd.MpoDate <= end
                         select new MpoHd
                         {
                             Id = hd.Id,
                             MpoNo = hd.MpoNo,
                             MpoDate=hd.MpoDate,
                             MpoType=hd.DeliAdd,
                             Attn=hd.Attn,
                             Heading=hd.Heading,
                             ShipMode=hd.ShipMode,
                             Shipment=hd.ShipDate,
                             Supplier=hd.SuppCode

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
          
           
            var txMpoHd = await _context.TxMpoHds.Include(det=>det.TxMpoDets).AsNoTracking().FirstOrDefaultAsync(hd=>hd.Id==id);

            #region linq
            //var result = from hd in _context.TxMpoHds
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
            //                 UDDate1 = hd.UDDate1,
            //                 UDField3 = hd.UDField3,
            //                 AllowPurchase = hd.AllowPurchase,
            //                 TxMpoDets = hd_det.ToList(),
            //             };

            //return await result.FirstOrDefaultAsync();
            #endregion

            if (txMpoHd == null)
            {
                return NotFound();
            }

            return txMpoHd;
        }
        [HttpGet("GetmpoView")]
        public async Task<ActionResult<IEnumerable<GenPOData>>> GetMpoView(string jobNo)
        {
            var result = await Task.Run(() => { return GenMpoBiz.GetPoData(jobNo); }); //GetMpoView(jobNo);
            if (result == null)
            {
                return NotFound();
            }
            return result;
           
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

        // POST: api/Mpo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Save")]
        public async Task<ActionResult<TxMpoHd>> PostTxMpoHd([FromBody]TxMpoHd txMpoHd)
        {
          if (_context.TxMpoHds == null)
          {
              return Problem("Entity set 'RMPortalContext.TxMpoHds'  is null.");
          }
            _context.TxMpoHds.Add(txMpoHd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {

            }
            

            return CreatedAtAction("GetTxMpoHd", new { id = txMpoHd.Id }, txMpoHd);
        }

        // DELETE: api/Mpo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTxMpoHd(int id)
        {
            if (_context.TxMpoHds == null)
            {
                return NotFound();
            }
            var txMpoHd = await _context.TxMpoHds.FindAsync(id);
            if (txMpoHd == null)
            {
                return NotFound();
            }

            _context.TxMpoHds.Remove(txMpoHd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TxMpoHdExists(int id)
        {
            return (_context.TxMpoHds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
