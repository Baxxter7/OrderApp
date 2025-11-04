using Microsoft.EntityFrameworkCore;
using OrderApp.Business.Mapping;
using OrderApp.Business.Services;
using OrderApp.Domain.Interfaces;
using OrderApp.Infraestructure.Data;
using OrderApp.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Database
builder.Services.AddDbContext<AppDbContext>(
    options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Mapper
builder.Services.AddAutoMapper(cfg => {}, typeof(OrdenMappingProfile));

//Dependency Injection
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

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