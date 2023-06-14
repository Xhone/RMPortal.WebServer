using Microsoft.Data.SqlClient;

namespace RMPortal.WebServer.Models
{
    public class Dept
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        public void Test(WebApplicationBuilder builder)
        {
            string? sgsConn = builder.Configuration.GetConnectionString("SGS");
            SqlConnection? conn = null;
            try
            {
                using (conn = new SqlConnection(sgsConn))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spTxMpoHdLd";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                        {new SqlParameter("@MpoNo",System.Data.SqlDbType.VarChar,20) };
                        parameters[0].Value = "OF22-00786";
                        cmd.Parameters.AddRange(parameters);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        System.Data.DataSet ds = new System.Data.DataSet();
                        adapter.Fill(ds);
                        var dt = ds.Tables[0];
                    }




                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                var mesg = ex.StackTrace;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
