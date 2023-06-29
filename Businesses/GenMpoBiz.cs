using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Helpers;
using System.Data;
using System.Reflection;

namespace RMPortal.WebServer.Businesses
{
    public class GenMpoBiz
    {
        public static List<GenPOData> GetPoData(string jobNo)
        {
            List<GenPOData> genPODatas = new List<GenPOData>();
            try
            {
                int userId = 1793;
                using(var dbContext=new SGSContext())
                {
                    
                    var userParam = new SqlParameter("@UserId", userId);
                    var result=dbContext.Database.ExecuteSqlRaw($"EXECUTE dbo.spGenMpoDelBeforeApply @UserId", userParam);


                    var txnNo = new SqlParameter("@TxnNo",jobNo);
                    var txnType=new SqlParameter("@TxnType","J");
                    var matCode=new SqlParameter("@MatCode", "");
                    var type=new SqlParameter("@Type", "");
                    var frMrDate=new SqlParameter("@FrMrDate", "1899-12-30 00:00:00");
                    var toMrDate=new SqlParameter("@ToMrDate", "1899-12-30 00:00:00");
                    var salesTeamList=new SqlParameter("@SalesTeamList", "");

                    result=dbContext.Database.ExecuteSqlRaw($"EXECUTE dbo.spGenMpoGetMrNo @UserId,@TxnNo,@TxnType,@MatCode,@Type,@FrMrDate,@ToMrDate,@SalesTeamList", userParam,txnNo,txnType,matCode, type,frMrDate,toMrDate,salesTeamList);

                    SqlParameter[] parameters = new SqlParameter[]
                     {
                           new SqlParameter("@UserId",System.Data.SqlDbType.Int),
                           new SqlParameter("@MatCode",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Type",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@SubType",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Category",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@SubCategory",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@FrMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@SalesTeamId",System.Data.SqlDbType.Int),
                           new SqlParameter("@FrExpectRecDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToExpectRecDate",System.Data.SqlDbType.DateTime)
                     };
                    parameters[0].Value = 1793;
                    parameters[1].Value = "!";
                    parameters[2].Value = "!";
                    parameters[3].Value = "!";
                    parameters[4].Value = "!";
                    parameters[5].Value = "!";
                    parameters[6].Value = "1899-12-30 00:00:00";
                    parameters[7].Value = "1899-12-30 00:00:00";
                    parameters[8].Value = 0;
                    parameters[9].Value = "1899-12-30 00:00:00";
                    parameters[10].Value = "1899-12-30 00:00:00";

                    genPODatas=  dbContext.GenPODatas.FromSqlRaw($"EXECUTE dbo.spGenPOLoad @UserId,@MatCode,@Type,@SubType,@Category,@SubCategory,@FrMrDate,@ToMrDate,@SalesTeamId,@FrExpectRecDate,@ToExpectRecDate", parameters).ToList();
                    
                    return genPODatas;
                }
            }
            catch
            {
               
            }
            return genPODatas;
        }
        public static List<GenPOData> GenMpoData(string jobNo)
        {
            List<GenPOData> genPODatas = new List<GenPOData>();
            try
            {
               
                ConnectionStrings connectionStrings = AppSettingsHelper.ReadObject<ConnectionStrings>("ConnectionStrings");
                connectionStrings.SGS+= "Password=e119769";
                using (SqlConnection conn= new SqlConnection(connectionStrings.SGS))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spGenMpoDelBeforeApply";//"select Id,MpoDetId,MpoNo,Qty from TxMpoDet where Id=@Id";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                        {new SqlParameter("@UserId",System.Data.SqlDbType.Int) };
                        parameters[0].Value = 1793;
                        cmd.Parameters.AddRange(parameters);
                        var res = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        //System.Data.DataSet ds = new System.Data.DataSet();
                        //adapter.Fill(ds, "MpoHdDet");//
                        ////ds.Tables[0].
                        //var dt = ds.Tables[0];


                    }
                    using(SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spGenMpoGetMrNo";
                        cmd.CommandType= System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                       {
                           new SqlParameter("@UserId",System.Data.SqlDbType.Int), 
                           new SqlParameter("@TxnNo",System.Data.SqlDbType.VarChar,30),
                           new SqlParameter("@TxnType",System.Data.SqlDbType.Char,1),
                           new SqlParameter("@MatCode",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Type",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@FrMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@SalesTeamList",System.Data.SqlDbType.VarChar,100)
                       };
                        parameters[0].Value = 1793;
                        parameters[1].Value = jobNo;
                        parameters[2].Value = "J";
                        parameters[3].Value = "";
                        parameters[4].Value = "";
                        parameters[5].Value = "1899-12-30 00:00:00";
                        parameters[6].Value = "1899-12-30 00:00:00";
                        parameters[7].Value = "";
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spGenPOLoad";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                       {
                           new SqlParameter("@UserId",System.Data.SqlDbType.Int),
                           new SqlParameter("@MatCode",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Type",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@SubType",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Category",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@SubCategory",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@FrMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@SalesTeamId",System.Data.SqlDbType.Int),
                           new SqlParameter("@FrExpectRecDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToExpectRecDate",System.Data.SqlDbType.DateTime)
                       };
                        parameters[0].Value = 1793;
                        parameters[1].Value = "!";
                        parameters[2].Value = "!";
                        parameters[3].Value = "!";
                        parameters[4].Value = "!";
                        parameters[5].Value = "!";
                        parameters[6].Value = "1899-12-30 00:00:00";
                        parameters[7].Value = "1899-12-30 00:00:00";
                        parameters[8].Value = 0;
                        parameters[9].Value = "1899-12-30 00:00:00";
                        parameters[10].Value = "1899-12-30 00:00:00";
                        cmd.Parameters.AddRange(parameters);
                        var res = cmd.ExecuteScalar();
                       
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        System.Data.DataSet ds = new System.Data.DataSet();
                        adapter.Fill(ds);//
                       
                        genPODatas = ConvertDataTable<GenPOData>(ds.Tables[0]);
                    }
                    conn.Close();
                }
            }
            catch(SqlException ex)
            {

            }
           
            return genPODatas;
        }

        public static List<MpoView> GetMpoView(string jobNo)
        {

            try
            {

                ConnectionStrings connectionStrings = AppSettingsHelper.ReadObject<ConnectionStrings>("ConnectionStrings");
                connectionStrings.SGS += "Password=e119769";
                using (SqlConnection conn = new SqlConnection(connectionStrings.SGS))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spGenMpoDelBeforeApply";//"select Id,MpoDetId,MpoNo,Qty from TxMpoDet where Id=@Id";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                        {new SqlParameter("@UserId",System.Data.SqlDbType.Int) };
                        parameters[0].Value = 1793;
                        cmd.Parameters.AddRange(parameters);
                        var res = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        //System.Data.DataSet ds = new System.Data.DataSet();
                        //adapter.Fill(ds, "MpoHdDet");//
                        ////ds.Tables[0].
                        //var dt = ds.Tables[0];


                    }
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spGenMpoGetMrNo";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                       {
                           new SqlParameter("@UserId",System.Data.SqlDbType.Int),
                           new SqlParameter("@TxnNo",System.Data.SqlDbType.VarChar,30),
                           new SqlParameter("@TxnType",System.Data.SqlDbType.Char,1),
                           new SqlParameter("@MatCode",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Type",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@FrMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@SalesTeamList",System.Data.SqlDbType.VarChar,100)
                       };
                        parameters[0].Value = 1793;
                        parameters[1].Value = jobNo;
                        parameters[2].Value = "J";
                        parameters[3].Value = "";
                        parameters[4].Value = "";
                        parameters[5].Value = "1899-12-30 00:00:00";
                        parameters[6].Value = "1899-12-30 00:00:00";
                        parameters[7].Value = "";
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "spGenPOLoad";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] parameters = new SqlParameter[]
                       {
                           new SqlParameter("@UserId",System.Data.SqlDbType.Int),
                           new SqlParameter("@MatCode",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Type",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@SubType",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@Category",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@SubCategory",System.Data.SqlDbType.VarChar,100),
                           new SqlParameter("@FrMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToMrDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@SalesTeamId",System.Data.SqlDbType.Int),
                           new SqlParameter("@FrExpectRecDate",System.Data.SqlDbType.DateTime),
                           new SqlParameter("@ToExpectRecDate",System.Data.SqlDbType.DateTime)
                       };
                        parameters[0].Value = 1793;
                        parameters[1].Value = "!";
                        parameters[2].Value = "!";
                        parameters[3].Value = "!";
                        parameters[4].Value = "!";
                        parameters[5].Value = "!";
                        parameters[6].Value = "1899-12-30 00:00:00";
                        parameters[7].Value = "1899-12-30 00:00:00";
                        parameters[8].Value = 0;
                        parameters[9].Value = "1899-12-30 00:00:00";
                        parameters[10].Value = "1899-12-30 00:00:00";
                        cmd.Parameters.AddRange(parameters);
                        var res = cmd.ExecuteScalar();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        System.Data.DataSet ds = new System.Data.DataSet();
                        adapter.Fill(ds);//

                        List<MpoView> genPODatas = ConvertDataTable<MpoView>(ds.Tables[0]);
                        return genPODatas;
                    }
                    
                }
            }
            catch (SqlException ex)
            {
               
            }

            return new List<MpoView>();
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T item = GetItem<T>(dr);
                list.Add(item);
            }

            return list;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type type = typeof(T);
            T obj=Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns) 
            {
                foreach(PropertyInfo pro in type.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                       
                        if (dr[column.ColumnName] == DBNull.Value)
                        {
                            pro.SetValue(obj, null, null);
                        }else
                            pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
