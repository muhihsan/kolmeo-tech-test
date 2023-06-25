using API.Infrastructure.Configurations;
using API.Repositories;
using API.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>(); // register validators
builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("Local"));

builder.Services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.MapControllers();

// TODO: Write/add middleware to log unexpected error
// TODO: Write/add middleware to retrieve correlationId from the request

app.Run();

