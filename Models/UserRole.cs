namespace RMPortal.WebServer.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoleId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
