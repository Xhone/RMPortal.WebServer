using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ui_policy", policy =>
    {
        policy.WithOrigins("http://localhost:8080");
    });
});
// Add services to the container.

builder.Services.AddControllers();
string sgsConn = builder.Configuration.GetConnectionString("SGS");
SqlConnection conn=null;
try
{
    using (conn= new SqlConnection(sgsConn))
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
            var dt= ds.Tables[0];
        }



        
        conn.Close();
    }
}
catch(SqlException ex)
{
    var mesg = ex.StackTrace;
}
finally
{
    conn.Close();
}
//注册数据库
builder.Services.AddDbContext<RMPortalContext>(options =>
{
    string conn = builder.Configuration.GetConnectionString("RMContext");
    options.UseSqlServer(conn);
});
//添加数据库异常筛选器
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
