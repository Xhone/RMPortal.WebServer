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
using Microsoft.Extensions.Options;
using RMPortal.WebServer.Authorization;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Helpers;
using RMPortal.WebServer.Models.Sys;

namespace RMPortal.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly RMPortalContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly Secrets _secrets;
        public UserController(RMPortalContext context,IOptions<Secrets> options/*,JwtUtils jwtutil*/)
        {
            _context = context;
            _secrets = options.Value;
            //_jwtUtils = jwtutil;
        }
    
       

        // GET: api/User
        //[EnableCors("ui_policy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var username = Helpers.WindowsApi.GetCurrentUser();
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
            if(string.IsNullOrWhiteSpace(loginInfo.Password)) {
                return BadRequest(new JsonResult("password is empty."));
            }
            loginInfo.Password = loginInfo.Password.EncryptDES(_secrets.User);
            User? user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginInfo.Username && x.Password == loginInfo.Password);
            if (user == null)
            {
                
                return BadRequest(new JsonResult(new { message = "Username or password is incorrect" }));
            }

            //var token=_jwtUtils.GenerateJwtToken(user);
            //AuthenticateResponse response=new AuthenticateResponse(user,token); 
            loginInfo.Password = string.Empty;
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
            if (id != user.Id||string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest();
            }
            //User? temp=await _context.Users.FindAsync(id);
            //if (temp.Password!= user.Password.EncryptDES(_secrets.User)) {
                user.Password = user.Password.EncryptDES(_secrets.User);
           // }
            
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
            string ps = user.Password;
            user.Password=user.Password.EncryptDES(_secrets.User);
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
