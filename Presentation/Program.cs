using Business;
using Business.Interfaces;
using Business.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AirportContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IVisaService, VisaService>();
builder.Services.AddScoped<IBaggageService, BaggageService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITerminalService, TerminalService>();
builder.Services.AddScoped<IPlaneService, PlaneService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<IFlightService, FlightService>();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>                // Custom 404 page
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/NotFoundPage";
        await next();
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
