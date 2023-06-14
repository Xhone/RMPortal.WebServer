using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.Helpers;
using RMPortal.WebServer.Models.Mpo;

namespace RMPortal.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MpoDetController : ControllerBase
    {
        private readonly RMPortalContext _context;

        public MpoDetController(RMPortalContext context)
        {
            _context = context;
        }

        // GET: api/MpoDet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TxMpoDet>>> GetTxMpoDets()
        {
          if (_context.TxMpoDets == null)
          {
              return NotFound();
          }
            return await _context.TxMpoDets.ToListAsync();
        }

        // GET: api/MpoDet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TxMpoDet>> GetTxMpoDet(int id)
        {
          if (_context.TxMpoDets == null)
          {
              return NotFound();
          }
            try
            {
                ConnectionStrings connectionStrings = AppSettingsHelper.ReadObject<ConnectionStrings>("ConnectionStrings");
                using(SqlConnection conn=new SqlConnection(connectionStrings.RMContext))
                {
                    conn.Open();
                    using(SqlCommand cmd=conn.CreateCommand())
                    {
                        cmd.CommandText = "GetMpoById";//"select Id,MpoDetId,MpoNo,Qty from TxMpoDet where Id=@Id";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                        {new SqlParameter("@Id",System.Data.SqlDbType.Int) };
                        parameters[0].Value = id;
                        cmd.Parameters.AddRange(parameters);
                        var res=cmd.ExecuteScalar();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        System.Data.DataSet ds = new System.Data.DataSet();
                        adapter.Fill(ds, "MpoHdDet");//
                        //ds.Tables[0].
                        var dt = ds.Tables[0];
                        

                    }
                    conn.Close();
                }
                var txMpoDet = await _context.TxMpoDets.FindAsync(id);
                if (txMpoDet == null)
                {
                    return NotFound();
                }

                return txMpoDet;
            }
            catch (SqlException ex) { 
                return BadRequest(ex.Message);
            }
           

            
        }

        // PUT: api/MpoDet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTxMpoDet(int id, TxMpoDet txMpoDet)
        {
            if (id != txMpoDet.Id)
            {
                return BadRequest();
            }

            _context.Entry(txMpoDet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TxMpoDetExists(id))
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

        // POST: api/MpoDet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TxMpoDet>> PostTxMpoDet(TxMpoDet txMpoDet)
        {
          if (_context.TxMpoDets == null)
          {
              return Problem("Entity set 'RMPortalContext.TxMpoDets'  is null.");
          }
            _context.TxMpoDets.Add(txMpoDet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTxMpoDet", new { id = txMpoDet.Id }, txMpoDet);
        }

        // DELETE: api/MpoDet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTxMpoDet(int id)
        {
            if (_context.TxMpoDets == null)
            {
                return NotFound();
            }
            var txMpoDet = await _context.TxMpoDets.FindAsync(id);
            if (txMpoDet == null)
            {
                return NotFound();
            }

            _context.TxMpoDets.Remove(txMpoDet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TxMpoDetExists(int id)
        {
            return (_context.TxMpoDets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
