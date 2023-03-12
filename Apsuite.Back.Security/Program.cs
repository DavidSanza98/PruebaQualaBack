using Apsuite.Back.Service.Security;
using Apsuite.Back.Transversal.Injection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Assembly assembly = Assembly.GetExecutingAssembly();

IHost host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Directory.GetCurrentDirectory())
    .TuApWebApiHost<Startup>(assembly)
    .Build();

host.Run();

// CREATE STARTUP INSTANCE
var startup = new Startup(builder.Configuration, builder.Environment);

// CONFIGURE SERVICES 
startup.ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/* CONFIGURE LIFETIME */
startup.Configure(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
