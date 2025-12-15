using CQRSRentACar.CQRSPattern.Handlers.AboutHandlers;
using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers;
using CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers;
using CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers;
using CQRSRentACar.CQRSPattern.Handlers.SliderHandlers;
using CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers;
using CQRSRentACar.CQRSPattern.Handlers.BookingHandlers;
using CQRSRentACar.CQRSPattern.Handlers.StatisticHandlers;
using CQRSRentACar.CQRSPattern.Handlers.MessageHandlers;
using CQRSRentACar.CQRSPattern.Handlers.FuelPriceHandlers;
using CQRSRentACar.CQRSPattern.Queries.FuelPriceQueries;
using CQRSRentACar.Context;
using Microsoft.EntityFrameworkCore;
using CQRSRentACar.CQRSPattern.Handlers.AirportHandlers;
using CQRSRentACar.CQRSPattern.Handlers.DistanceHandlers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CarContext>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<GetSliderQueryHandler>();
builder.Services.AddScoped<GetFeatureQueryHandler>();
builder.Services.AddScoped<GetServiceQueryHandler>();
builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<GetEmployeeQueryHandler>();
builder.Services.AddScoped<GetTestimonialQueryHandler>();
builder.Services.AddScoped<GetBookingQueryHandler>();
builder.Services.AddScoped<GetBookingByIdQueryHandler>();
builder.Services.AddScoped<GetAvailableCarsQueryHandler>();
builder.Services.AddScoped<GetStatisticQueryHandler>();

builder.Services.AddScoped<CreateMessageCommandHandler>();
builder.Services.AddScoped<GetMessageQueryHandler>();
builder.Services.AddScoped<GetMessageByIdQueryHandler>();
builder.Services.AddScoped<UpdateMessageCommandHandler>();
builder.Services.AddScoped<RemoveMessageCommandHandler>();

builder.Services.AddScoped<CreateBookingCommandHandler>();
builder.Services.AddScoped<UpdateBookingCommandHandler>();
builder.Services.AddScoped<RemoveBookingCommandHandler>();

builder.Services.AddScoped<GetAirportsByCountryQueryHandler>();

builder.Services.AddHttpClient<GetFuelPricesQueryHandler>();
builder.Services.AddScoped<GetDistanceQueryHandler>();
builder.Services.AddHttpClient<GetDistanceQueryHandler>();





var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();