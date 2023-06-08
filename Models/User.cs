namespace RMPortal.WebServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
       
        public string Password { get; set; }
        public int DeptId { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string NTID { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
