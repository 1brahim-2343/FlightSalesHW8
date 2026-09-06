
using FlightSales.Application.Interfaces.Repositories;
using FlightSales.Application.Interfaces.Services;
using FlightSales.Application.Mappers;
using FlightSales.Infrastructure.Persistence;
using FlightSales.Infrastructure.Repositories;
using FlightSales.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace FlightSales.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<FlightSalesDbContext>(options =>
                options.UseSqlServer(builder.Configuration
                    .GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"]!;
            }, typeof(MappingProfile).Assembly);


            builder.Services.AddScoped<IFlightRepository, FlightRepository>();
            builder.Services.AddScoped<IFlightTicketRepository, FlightTicketRepository>();

            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IFlightTicketService, FlightTicketService>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
