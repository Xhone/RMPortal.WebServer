using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.Authorization;
using RMPortal.WebServer.Controllers;
using RMPortal.WebServer.Data;
using RMPortal.WebServer.Helpers;
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

//configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


//configure DI for application
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

//configure DI for application serice
builder.Services.AddScoped<IJwtUtils,JwtUtils>();


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

//configure HTTP request pipeline,global cors policy
//app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
app.UseCors();

app.MapControllers();
//custom jwt auth middleware
//app.UseMiddleware<JwtMiddleware>();

app.Run();
