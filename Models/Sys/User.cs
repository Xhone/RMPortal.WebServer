using System.Text.Json.Serialization;

namespace RMPortal.WebServer.Models.Sys
{
    public class User:CommonProp
    {
       
        public string? UserName { get; set; }
        //[JsonIgnore]
        public string? Password { get; set; }
        public int DeptId { get; set; }
        public string? Title { get; set; }
        public string? Level { get; set; }
        public string? NTID { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
       
    }

   
}
