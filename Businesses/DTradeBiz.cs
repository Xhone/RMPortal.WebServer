using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.Models.SGS;

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

        public static List<MaMatDetail>? GetMaMatDetail(string? matCode)
        {
            try
            {
                using (var dbContext = new SGSContext())
                {
                    SqlParameter[] sqlParams = new SqlParameter[]
                    {
                    new SqlParameter
                    {
                        ParameterName="MatCode",
                        Value=matCode,
                    },
                    new SqlParameter
                    {
                        ParameterName="SuppCode",
                        Value="",
                    }
                    };


                    var result = dbContext.MaMatDetails.FromSqlRaw($"EXECUTE dbo.spTxMpoGetMatCode @MatCode, @SuppCode", sqlParams).ToList();
                    return result;
                }
            }
          
                catch(SqlException ex)
                {
                    return null;
                }


        }
    }
}
