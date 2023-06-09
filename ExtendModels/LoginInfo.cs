using System.ComponentModel.DataAnnotations;

namespace RMPortal.WebServer.ExtendModels
{
    public class LoginInfo
    {
        [Display(Name ="Account")]
        [Required(ErrorMessage ="The account can not be empty")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The password can not be empty")]
        public string Password { get; set; }
    }
}
