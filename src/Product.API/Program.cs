using API.Infrastructure.Configurations;
using API.Repositories;
using API.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add structured logging

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Use real database
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

/* 
 * TODO: Add error handler middleware
 * This middleware can capture all errors ocurred on the app
 * Some things that can be placed here
 * - Logging all errors
 * - Return standard error format
*/

/*
 * TODO: Add middleware to add some requests info to the logs context
 * Some of the info to be added:
 * - X-Request-Id
 * - Method
 * - Route Name
 * - URL
 * - Latency
*/

app.Run();

