using EligiblesListingAPI.Core.Abstractions;
using EligiblesListingAPI.Core.Resources;
using EligiblesListingAPI.Infrastructure.Data.Services;
using EligiblesListingAPI.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EligiblesListingAPI", Version = "v1" });
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ICustomerCore, CustomerCore>();
builder.Services.AddSingleton<IEnrichCustomerCore, EnrichCustomerCore>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IDataLoadService, DataLoadService>();
var app = builder.Build();

var dataLoaderService = app.Services.GetRequiredService<IDataLoadService>();

dataLoaderService.SeedData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EligiblesListingAPI v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program { }