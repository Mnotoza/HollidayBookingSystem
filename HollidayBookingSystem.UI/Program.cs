using HollidayBookingSystem.Application.Handlers;
using HollidayBookingSystem.Domain.Events;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.Domain.Strategies;
using HollidayBookingSystem.Domain.Strategies.Interfaces;
using HollidayBookingSystem.Infrastructure;
using HollidayBookingSystem.Infrastructure.Events;

namespace HollidayBookingSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Domain + Infrastructure
            builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();

            // Observers
            builder.Services.AddSingleton<IBookingObserver, EmailNotifier>();
            builder.Services.AddSingleton<IBookingObserver, AuditLogger>();

            // Strategies
            builder.Services.AddSingleton<IDictionary<string, IBookingStrategy>>(sp => new Dictionary<string, IBookingStrategy>
            {
                { "Apartment", new ApartmentStrategy() },
                { "Vehicle", new VehicleStrategy() },
                { "Show", new ShowStrategy() }
            });

            // Application layer handler
            builder.Services.AddSingleton<BookingCommandHandler>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
