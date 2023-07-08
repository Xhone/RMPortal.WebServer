namespace RMPortal.WebServer.Models.SGS
{
    public class JobOrder
    {
        public string? JobNo { get; set; }
        public string? JobDate { get; set; }  
        public string? JobType { get; set; }
        public string? StyleNo { get; set; }
        public string? SuppCode { get; set; }
        public string? CustCode { get; set; }
        public string? CustStyle { get; set; }
        public string? Season { get; set; }
        public string? JobDeliDate { get; set; }
        public int? SalesTeamId { get; set; }
        public string? SalesTeamCode { get; set; }
    }
}
