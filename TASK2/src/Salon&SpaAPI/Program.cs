using Microsoft.EntityFrameworkCore;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using SalonSpa.Persistence;
using SalonSpa.API.Models;

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