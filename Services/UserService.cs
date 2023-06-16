using RMPortal.WebServer.Authorization;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Models.Sys;

namespace RMPortal.WebServer.Services
{
    public interface IUserService
    {
        AuthenticateResponse? Authenticate(LoginInfo loginInfo);
        IEnumerable<User> GetAll();
        User? GetById(int id);
    }
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
    {
        new User { Id = 1, UserName = "Test", Password = "test" }
    };

        private readonly IJwtUtils _jwtUtils;
        public UserService(IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }
        public AuthenticateResponse? Authenticate(LoginInfo loginInfo)
        {
            var user=_users.SingleOrDefault(x=>x.UserName== loginInfo.Username&&x.Password==loginInfo.Password);
            if (user == null)
            {
                return null;
            }
            var token=_jwtUtils.GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(x=>x.Id==id);
        }
    }
}
