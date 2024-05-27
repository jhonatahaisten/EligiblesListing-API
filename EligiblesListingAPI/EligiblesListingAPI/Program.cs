//using Microsoft.EntityFrameworkCore;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Application.Services;
using EligiblesListingAPI.Domain.Interfaces;
using EligiblesListingAPI.Infrastructure.Data;
using EligiblesListingAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, DataService>();
builder.Services.AddScoped<IGetEligibleCustomersQuery, GetEligibleCustomersQuery>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
