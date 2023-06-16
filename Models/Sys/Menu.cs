namespace RMPortal.WebServer.Models.Sys
{
    public class Menu:CommonProp
    {
       
        public string? Name { get; set; }
        public int ParentId { get; set; }
        public string? Url { get; set; }
        public string? Perms { get; set; }
      
    }
}
