using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;



namespace HolidayBookingSystem.Tests
{
    // Option A: fully qualify the app Program
    public class CustomWebApplicationFactory : WebApplicationFactory<HollidayBookingSystem.UI.Program>
    {
        public Mock<IBookingRepository> MockRepository { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the real repository registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IBookingRepository));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Add the mock repository
                services.AddSingleton(MockRepository.Object);
            });
        }
    }
    public class BookingFlowTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly Mock<IBookingRepository> _mockRepo;

        public BookingFlowTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mockRepo = factory.MockRepository;
        }

        [Fact]
        public async Task BookingFlow_Create_CallsRepository()
        {
            var formData = new Dictionary<string, string>
        {
            { "Input.CustomerName", "Test User" },
            { "Input.BookingDate", "2026-09-28" },
            { "Input.Type", "Apartment" }
        };

            var content = new FormUrlEncodedContent(formData);
            var response = await _client.PostAsync("/AddBooking", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            // Verify repository interaction
            _mockRepo.Verify(r => r.AddBookingAsync(It.IsAny<Booking>()), Times.Once);
        }
    }
}