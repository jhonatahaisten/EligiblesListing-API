//using Microsoft.EntityFrameworkCore;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Application.Services;
using EligiblesListingAPI.Domain.Interfaces;
using EligiblesListingAPI.Infrastructure.Data;
using EligiblesListingAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
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
