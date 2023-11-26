using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.Controllers;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaxCalculatorAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaxCalculatorAPIContext") ?? throw new InvalidOperationException("Connection string 'TaxCalculatorAPIContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
