using MediatR;
using Microsoft.EntityFrameworkCore;
using VipManagement.Application.Cards.Commands;
using VipManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ✅ EF
builder.Services.AddDbContext<VipManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ✅ MediatR - escaneja VipManagement.Application
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCardCommandHandler).Assembly));

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

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