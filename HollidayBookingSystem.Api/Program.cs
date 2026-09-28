using HollidayBookingSystem.Application.Handlers;
using HollidayBookingSystem.Domain.Events;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.Domain.Strategies;
using HollidayBookingSystem.Domain.Strategies.Interfaces;
using HollidayBookingSystem.Infrastructure;
using HollidayBookingSystem.Infrastructure.Decorators;
using HollidayBookingSystem.Infrastructure.Events;

var builder = WebApplication.CreateBuilder(args);

// Add framework services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();
builder.Services.Decorate<IBookingRepository, LoggingRepositoryDecorator>();

// Register observers
builder.Services.AddSingleton<IBookingObserver, EmailNotifier>();
builder.Services.AddSingleton<IBookingObserver, AuditLogger>();

// Register strategies
builder.Services.AddSingleton<IBookingStrategy, ApartmentStrategy>();
builder.Services.AddSingleton<IBookingStrategy, VehicleStrategy>();
builder.Services.AddSingleton<IBookingStrategy, ShowStrategy>();

// Register dictionary factory for strategies
builder.Services.AddSingleton<IDictionary<string, IBookingStrategy>>(provider =>
{
    var strategies = provider.GetServices<IBookingStrategy>();
    return strategies.ToDictionary(
        s => s.GetType().Name.Replace("Strategy", ""),
        s => s
    );
});

// Register handler
builder.Services.AddSingleton<BookingCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
