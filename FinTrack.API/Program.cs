using FinTrack.API.Data;
using Microsoft.EntityFrameworkCore;
using FinTrack.API.Interfaces;
using FinTrack.API.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=fintrackdb;Username=talhapakdil"));

// Controller sistemini açar
builder.Services.AddControllers();

// Swagger için gerekli servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITransactionService, TransactionService>();
var app = builder.Build();


// Development ortamında Swagger UI açılır
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS yönlendirmesi
app.UseHttpsRedirection();

// Controllers klasöründeki controller endpointlerini aktif eder
app.MapControllers();



app.Run();