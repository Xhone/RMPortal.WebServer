namespace RMPortal.WebServer.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Url { get; set; }
        public string Perms { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
