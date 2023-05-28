using APIMySql—oursework.DBMySql;
using APIMySql—oursework.Hubs;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSession();
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen();*/
builder.Services.AddSignalR();  
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<DBConnection>(_ => new DBConnection(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

app.MapHub<SignalRHubShedules>("/signalRHubShedules");
app.MapHub<SignalRHubOrderStatus>("/signalRHubOrderStatus");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseSession();

app.MapControllers();
app.MapDefaultControllerRoute();
app.Run();
