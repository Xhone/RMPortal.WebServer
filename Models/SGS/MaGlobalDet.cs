namespace RMPortal.WebServer.Models.SGS
{
    public class MaType
    {
        public string? MpoType { get; set; }    

    }

    public class MaHead
    {
        public string? Head
        {
            get;set; 
        }
    }

    public class MaGlobalDet
    {
        public int KeyNo { get; set; }
        public int Seq { get; set; }
        public int KeyId { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set;}
        public string? CreateUser { get; set;}
    }
}
