using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RMPortal.WebServer.Data;

namespace RMPortal.WebServer.Businesses
{
    public class DTradeBiz
    {
        public static int GetDtradeUser(string userName)
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    var result = dbContext.Database.SqlQuery<int>($"SELECT UserID AS Value  FROM UtUser WHERE Lock='N' AND FullName={userName}").FirstOrDefault();
                    return result;
                }
            }
            catch(SqlException ex) { }
            {
                return 1;
            }
        }
    }
}
