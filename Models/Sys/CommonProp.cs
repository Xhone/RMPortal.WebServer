namespace RMPortal.WebServer.Models.Sys
{
    public class CommonProp
    {
        public int Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
