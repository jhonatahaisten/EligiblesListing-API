using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Interfaces;
using EligiblesListingAPI.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

//builder.Services.AddSingleton<ICustomerRepository, EligiblesListingAPI.Infrastructure.Repositories.CustomerService>();
builder.Services.AddSingleton<ICustomerService, EligiblesListingAPI.Application.Services.CustomerService>();
builder.Services.AddSingleton<IDataService, DataService>();
//builder.Services.AddScoped<IGetEligibleCustomersQuery, GetEligibleCustomersQuery>();

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
