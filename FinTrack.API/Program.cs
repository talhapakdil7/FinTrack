using FinTrack.API.Data;
using FinTrack.API.Helpers;
using FinTrack.API.Interfaces;
using FinTrack.API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// appsettings.json içinden connection string okunur
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// appsettings.json içinden JWT ayarları okunur
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

// DATABASE CONNECTION
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// CONTROLLERS
builder.Services.AddControllers();

// VALIDATION
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTransactionDtoValidator>();

// SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DEPENDENCY INJECTION
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// JWT AUTHENTICATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
       ValidateIssuer = false,
ValidateAudience = false,
ValidateLifetime = true,
ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = audience,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey!)
        )
    };
});

var app = builder.Build();

// DEVELOPMENT ortamında Swagger açılır
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Sıralama önemli
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();