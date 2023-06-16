namespace RMPortal.WebServer.Helpers
{
    public class AppSettings
    {
        public string? Secret { get; set; }
    }

    public class Secrets
    {
        public string? JWT { get; set; }
        public string? User { get; set; }
    }
}
