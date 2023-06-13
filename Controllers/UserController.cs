using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Authorization;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Models;

namespace RMPortal.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly RMPortalContext _context;
        private readonly IJwtUtils _jwtUtils;
        public UserController(RMPortalContext context)
        {
            _context = context;
          
        }
    
       

        // GET: api/User
        //[EnableCors("ui_policy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        //[EnableCors("ui_policy")]
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        //[EnableCors("ui_policy")]
        //[AllowAnonymous]
        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody]LoginInfo loginInfo)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginInfo.Username && x.Password == loginInfo.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            //var token=_jwtUtils.GenerateJwtToken(user);
            //AuthenticateResponse response=new AuthenticateResponse(user,token); 
            return Ok(Response.StatusCode);
        }

        //[EnableCors("ui_policy")]
        //[AllowAnonymous]
        [HttpPost("login2")]
        
        public async Task<IActionResult> Login(string username,string password)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == username && x.Password == password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            //var token=_jwtUtils.GenerateJwtToken(user);
            //AuthenticateResponse response=new AuthenticateResponse(user,token); 
            return Ok(user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
