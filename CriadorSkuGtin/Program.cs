using System.Text.Json.Serialization;
using CriadorSkuGtin.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
	.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

string connString = $"server=192.168.10.250;userid=SkuGtinUser;password=SKUp455w0rd;database=skugtingenerator;ConvertZeroDateTime=True";
ServerVersion version = new MySqlServerVersion(MySqlServerVersion.LatestSupportedServerVersion);
Action<DbContextOptionsBuilder> configureDbContext = c =>
{
	c.UseMySql(connString, version, x =>
	{
		x.CommandTimeout(600);
		x.EnableRetryOnFailure(3);
	});
	c.EnableSensitiveDataLogging();
};
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
	dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
