using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.Models.SGS;
using RMPortal.WebServer.Models.Sys;

namespace RMPortal.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SGSController:ControllerBase
    {
        [HttpGet("GetMpoType")]
        public async Task<ActionResult<IEnumerable<MaType>>> GetMpoType()
        {
            try
            {
                using (var dbContext = new SGSContext())

                {
                    var id = 70151;
                    var result = await dbContext.MaTypes.FromSql($"select Field1 as MpoType from MaGlobalDet where KeyNo={id}").ToListAsync();

                    return Ok(result);
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            
        }
        [HttpGet("GetMpoHeading")]
        public async Task<ActionResult<IEnumerable<string>>> GetMpoHeading()
        {
            try
            {
                using(var dbContext = new SGSContext())
                {
                    var result=await dbContext.Database.SqlQuery<string>($"Select SC_Heading from MaSCHeading").ToListAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
