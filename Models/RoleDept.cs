namespace RMPortal.WebServer.Models
{
    public class RoleDept
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int DeptId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
