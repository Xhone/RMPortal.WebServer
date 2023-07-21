using Microsoft.Data.SqlClient;
using System.Data;

namespace RMPortal.WebServer.Util
{
    public class SqlUtil
    {

        //定义数据库连接字符串
        //public static string Str_sqlcon1 = "server=hksvs012;Initial Catalog=DtradeSimpleGarment01;User Id=admin;Password=admin123;Integrated Security=True;TrustServerCertificate=true;";
        //public static string Str_sqlcon1 = "server=(local);Initial Catalog=DarGermentTest;Integrated Security=True;TrustServerCertificate=true;";
        //public static string Str_sqlcon2 = "Data Source=.;Database=D_total;Userid=sa;PWD=123";
        public static string Str_sqlcon3 = "Data Source =hksvs012;Initial Catalog=DtradeSimpleGarment01;User Id=admin;Password=admin123;Encrypt=True;TrustServerCertificate=True;";
        public static string Str_sqlcon4 = "Data Source =hssvs117;Initial Catalog=DtradeSimpleGarment01;Trusted_Connection = SSPI;Encrypt=True;TrustServerCertificate=True;";



        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection getcon(SqlConnection My_con, string sqlDataStr)
        {
            My_con = new SqlConnection(sqlDataStr);//用SqlConnection对象与指定的数据库相连接  
            try
            {
                My_con.Open();//打开数据库连接
            }
            catch
            {
                new Exception("连接数据库失败");
            }

            return My_con;//返回SqlConnection对象信息
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void con_close(SqlConnection My_con)
        {
            if (My_con.State == ConnectionState.Open)//判断是否打开与数据库的连接
            {
                My_con.Close();//关闭数据库的连接
                My_con.Dispose();//释放My_con变量的所有空间
            }
        }
        /// <summary>
        /// 以只读方式读取数据库信息
        /// </summary>
        /// <param name="SQLstr">表示传递的SQL语句</param>
        /// <returns></returns>
        public SqlDataReader getcom(string SQLstr, string sqlDataStr)
        {
            SqlConnection My_con = null;
            My_con = getcon(My_con, sqlDataStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4);//打开数据库连接
            SqlCommand My_com = My_con.CreateCommand();//创建SqlConnection对象，用于执行SQL语句
            My_com.CommandText = SQLstr;//获取指定的SQL语句
            SqlDataReader My_read = My_com.ExecuteReader();//执行SQL语句，生成一个SqlDataReader对象
            con_close(My_con);
            return My_read;
        }
        /// <summary>
        /// 通过SqlCommand对象执行数据库中的添加、修改和删除操作
        /// </summary>
        /// <param name="SQLstr">表示传递的SQL语句</param>
        public void getsqlcom(string SQLstr, string sqlDataStr)
        {
            SqlConnection My_con = null;
            My_con = getcon(My_con, sqlDataStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4);//打开数据库连接
            SqlCommand SQLcom = new SqlCommand(SQLstr, My_con);//创建SqlConnection对象，用于执行SQL语句
            SQLcom.ExecuteNonQuery();//执行SQL语句
            SQLcom.Dispose();//释放所有空间
            con_close(My_con);//关闭数据库连接
        }
        /// <summary>
        /// 通过SqlCommand对象执行数据库中的添加、修改和删除操作
        /// </summary>
        /// <param name="SQLstr">传递的SQL语句</param>
        /// <param name="tableName"></param>
        /// <returns>DataSet</returns>
        public DataSet getDataSet(string SQLstr, string tableName, string sqlDataStr)
        {
            SqlConnection My_con = null;
            My_con = getcon(My_con, sqlDataStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4);//打开数据库连接
            SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);
            DataSet ds = new DataSet();//创建DataSet对象
            SQLda.Fill(ds, tableName);
            con_close(My_con);//关闭数据库连接
            return ds;//返回DataSet对象信息
        }
        /// <summary>
        /// 通过SqlCommand对象执行数据库中的添加、修改和删除操作
        /// </summary>
        /// <param name="SQLstr">传递的SQL语句</param>
        /// <param name="tableName"></param>
        /// <returns>DataTable</returns>
        public DataTable getDataTable(string SQLstr, string tableName, string sqlDataStr)
        {
            SqlConnection My_con = null;
            DataTable dt = new DataTable();
            My_con = getcon(My_con, sqlDataStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4);//打开数据库连接
            SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);
            DataSet ds = new DataSet();
            SQLda.Fill(ds, tableName);
            dt = ds.Tables[tableName];
            con_close(My_con);//关闭数据库连接
            return dt;//返回DataSet对象信息
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="csName">存储过程参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="sqlDataStr">数据库参数值</param>
        /// <returns></returns>
        public DataTable getDataTable(string procedureName, string csName, string value, string sqlDataStr)
        {
            SqlConnection My_con = My_con = new SqlConnection(sqlDataStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4);//用SqlConnection对象与指定的数据库相连接  
            try
            {
                My_con.Open();//打开数据库连接
            }
            catch
            {
                new Exception("连接数据库失败");
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = My_con;
            da.SelectCommand.CommandText = procedureName;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter(csName, value);
            param.Direction = ParameterDirection.Input;
            param.Value = value;
            da.SelectCommand.Parameters.Add(param);
            DataTable mytable = new DataTable();
            da.Fill(mytable);
            con_close(My_con);
            return mytable;
        }
        /// <summary>
        /// 
        ///     无参数存储过程调用
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <returns></returns>
        public DataTable getDataTable(string procedureName, string sqlDataStr)
        {
            SqlConnection My_con = new SqlConnection(sqlDataStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4);//用SqlConnection对象与指定的数据库相连接  
            try
            {
                My_con.Open();//打开数据库连接
            }
            catch
            {
                new Exception("连接数据库失败");
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = My_con;
            da.SelectCommand.CommandText = procedureName;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable mytable = new DataTable();
            da.Fill(mytable);
            con_close(My_con);
            return mytable;
        }

        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="sqlDataBaseStr">数据库字段</param>
        public bool doTransaction(string sqlStr, string sqlDataBaseStr)
        {
            using (SqlConnection conn = new SqlConnection(sqlDataBaseStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4))
            {
                conn.Open();
                SqlTransaction mytx = conn.BeginTransaction();
                using (SqlCommand scomm = new SqlCommand())
                {
                    if (sqlStr == "") return true;
                    try
                    {

                        scomm.Connection = conn;
                        scomm.CommandText = sqlStr;
                        scomm.Transaction = mytx;
                        scomm.ExecuteNonQuery();
                        mytx.Commit();
                        return true;
                    }
                    catch
                    {
                        mytx.Rollback();
                        return false;
                    }

                }
            }
        }

        public bool doTransaction(string[] sqlStrs, string sqlDataBaseStr)
        {
            using (SqlConnection conn = new SqlConnection(sqlDataBaseStr.Equals("Str_sqlcon3") ? Str_sqlcon3 : Str_sqlcon4))
            {
                conn.Open();
                SqlTransaction mytx = conn.BeginTransaction();
                using (SqlCommand scomm = new SqlCommand())
                {
                    try
                    {
                        scomm.Connection = conn;
                        scomm.Transaction = mytx;
                        foreach (string sqlStr in sqlStrs)
                        {
                            if (!sqlStr.Equals(""))
                            {
                                scomm.CommandText = sqlStr;
                                scomm.ExecuteNonQuery();
                            }
                        }
                        mytx.Commit();
                        return true;
                    }
                    catch
                    {
                        mytx.Rollback();
                        return false;
                    }

                }
            }
        }
    }
}
