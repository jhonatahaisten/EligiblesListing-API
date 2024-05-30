using EligiblesListingAPI.Core.Abstractions;
using EligiblesListingAPI.Core.Resources;
using EligiblesListingAPI.Infrastructure.Data.Services;
using EligiblesListingAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ICustomerCore, CustomerCore>();
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<IDataLoadService, DataLoadService>();
var app = builder.Build();

var dataLoaderService = app.Services.GetRequiredService<IDataLoadService>();

dataLoaderService.SeedData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
