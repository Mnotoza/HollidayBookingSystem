using HollidayBookingSystem.Application.Commands;
using HollidayBookingSystem.Application.Handlers;
using HollidayBookingSystem.Domain.Events;
using HollidayBookingSystem.Domain.Factories;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.Domain.Strategies;
using HollidayBookingSystem.Domain.Strategies.Interfaces;
using HollidayBookingSystem.Infrastructure;
using HollidayBookingSystem.Infrastructure.Decorators;
using HollidayBookingSystem.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;

namespace HollidayBookingSystem.ConsoleApp 
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Holiday Booking System started...");

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Register repositories
                    services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();
                    services.Decorate<IBookingRepository, LoggingRepositoryDecorator>();

                    // Register observers
                    services.AddSingleton<IBookingObserver, EmailNotifier>();
                    services.AddSingleton<IBookingObserver, AuditLogger>();

                    // Register strategies
                    services.AddSingleton<IBookingStrategy, ApartmentStrategy>();
                    services.AddSingleton<IBookingStrategy, VehicleStrategy>();
                    services.AddSingleton<IBookingStrategy, ShowStrategy>();

                    // Register dictionary factory
                    services.AddSingleton<IDictionary<string, IBookingStrategy>>(provider =>
                    {
                        var strategies = provider.GetServices<IBookingStrategy>();
                        return strategies.ToDictionary(
                            s => s.GetType().Name.Replace("Strategy", ""), 
                            s => s);
                    });

                    // Register handler
                    services.AddSingleton<BookingCommandHandler>();
                })
                .Build();

            // Resolve handler from DI
            var handler = host.Services.GetRequiredService<BookingCommandHandler>();

            // Repository with logging decorator
            IBookingRepository bookingRepository = new LoggingRepositoryDecorator(new InMemoryBookingRepository());

            // Observers (Infrastructure implementations)
            var observers = new List<IBookingObserver>
            {
                new EmailNotifier(),
                new AuditLogger()
            };

            // Strategies (Domain implementations)
            var strategies = new Dictionary<string, IBookingStrategy>
            {
                { "Apartment", new ApartmentStrategy() },
                { "Vehicle", new VehicleStrategy() },
                { "Show", new ShowStrategy() }
            };

            // Handler orchestrates commands
      
            // Add booking
            var booking = BookingFactory.Create("Apartment", "Alice", DateTime.Now);
            await handler.HandleAsync(new AddBookingCommand(bookingRepository, booking));

            //// Edit booking
            //booking.CustomerName = "Alice Smith";
            //await handler.HandleAsync(new EditBookingCommand(bookingRepository, booking));

            //// Delete booking
            //await handler.HandleAsync(new DeleteBookingCommand(bookingRepository, booking.Id));

            Console.WriteLine("Holiday Booking System finished.");

        }
    }
}
