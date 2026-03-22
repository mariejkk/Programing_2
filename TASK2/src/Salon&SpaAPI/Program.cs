using Microsoft.EntityFrameworkCore;
using Salon_Spa.Application.Services;
using SalonSpa.Application.Models;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using SalonSpa.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalonSpaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}, typeof(Program).Assembly);

builder.Services.AddTransient<ServiceRepository>();
builder.Services.AddTransient<ServiceVariantRepository>();
builder.Services.AddTransient<ClientRepository>();
builder.Services.AddTransient<EmployeeRepository>();
builder.Services.AddTransient<AppointmentRepository>();
builder.Services.AddTransient<GenericRepository<AppointmentStatus>>();
builder.Services.AddTransient<GenericRepository<PaymentMethod>>();
builder.Services.AddTransient<UnitOfWork>();

builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<SalonService>();
builder.Services.AddTransient<AppointmentService>();

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