using System.Data;
using Kernel.Application.Abstractions.Data;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Reservation.Application.Rooms.Commands;
using Reservation.Domain.Rooms.Interfaces;
using Reservation.Persistence;
using Reservation.Persistence.Room.Repositories;
using Reservation.Persistence.Services;
using Reservation.Persistence.Mapping;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(cfg => { }, typeof(RoomProfile).Assembly);

// DbContext
builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IDbConnection per queries tipus Dapper / SQL directe
builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateRoomCommandHandler).Assembly));

// Repositories / UnitOfWork
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();