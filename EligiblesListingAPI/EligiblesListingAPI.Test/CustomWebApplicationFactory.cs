using EligiblesListingAPI.Core.Abstractions;
using EligiblesListingAPI.Domain.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;


namespace EligiblesListingAPI.Test
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TEntryPoint>();
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's IDataLoadService registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IDataLoadService));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a mock IDataLoadService
                var mockDataLoadService = new Mock<IDataLoadService>();
                mockDataLoadService.Setup(m => m.GetAllCustomers()).Returns(GetTestCustomers());

                services.AddSingleton(mockDataLoadService.Object);
            });
        }

        private List<Customer> GetTestCustomers()
        {
            return new List<Customer>
            {
                // Add test data here
            };
        }
    }
}