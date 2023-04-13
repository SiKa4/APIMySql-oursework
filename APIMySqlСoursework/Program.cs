using APIMySql—oursework.DBMySql;
using APIMySql—oursework.Hubs;
using Microsoft.AspNet.SignalR.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen();*/
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<DBConnection>(_ => new DBConnection(builder.Configuration["ConnectionStrings:DefaultConnection"]));
var app = builder.Build();
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.MapHub<SignalRHubShedules>("/signalRHubShedules");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
