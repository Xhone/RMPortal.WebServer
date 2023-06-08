namespace RMPortal.WebServer.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
