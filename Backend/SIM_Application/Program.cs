using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SIM_Application.Models;
using SIM_Application.Repository;

var builder = WebApplication.CreateBuilder(args);
//WebApplication - class provided by ASP.NET Core for creating web applications.
//entry point for configuring and running an ASP.NET Core web application.
//CreateBuilder(args) - static method of the WebApp cls.
//creates instance of WebAppBuilder, which is a builder cls used to configure the ASP.NET Core application during startup.
//args - command-line arguments passed to the application..

// Add services to the container.
builder.Services.AddControllersWithViews();
//Services - property provides access to the collection of services available within the app
//AddControllersWithViews - extension method, configures the services required to use controllers and views in the application
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin", builder =>
//    {
//        builder.WithOrigins("http://localhost:4200")
//        .AllowAnyHeader().AllowAnyMethod();
//    });
//}  
//);
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigin", builder =>
	{
		builder.WithOrigins("http://localhost:4200")
			   .AllowAnyHeader()
			   .AllowAnyMethod();
	});
});
builder.Services.AddDbContext<StockInventoryManagementSystemContext>((serviceProvider, options) =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"),
						 sqlServerOptions =>
						 {
							 sqlServerOptions.EnableRetryOnFailure();
						 });
});


//AddDbContext - used to register the database context class
//<StockInventoryManagementSystemContext> - database context type
//options - represents the options for configuring the database context.(instructions)
//UseSqlServer - to configures the database provider
//builder.Configuration.GetConnectionString("DbContext") - retrieves connection string named "DbCon" from the application's configuration
builder.Services.AddScoped<IUserDetailRepository,UserDetailRepository>();
builder.Services.AddScoped<IBankDetailRepository,BankDetailRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/Login/Login";
	options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});
var _logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(_logger);
var app = builder.Build();
//to finalize app's configuration, services and middleware pipeline

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())//checks whether the current environment is not set to "Development"
{
    app.UseExceptionHandler("/Home/Error");//configures the application to use an error handling middleware to handle exceptions
    //redirect to the "/Home/Error" to display a friendly error page to the user.
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //enables HTTP Strict Transport Security (HSTS), security feature that informs browsers to only communicate with the server over HTTPS
}

app.UseHttpsRedirection();//to automatically redirect HTTP requests to HTTPS
app.UseStaticFiles();//enables serving static files, such as HTML, CSS, JavaScript, images

app.UseRouting();//sets up routing for incoming requests

app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
// to authenticate incoming requests and then authorize them based on the provided credentials and access policies

app.MapControllerRoute(//default page to render
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
