using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Implementations;
using QuantityMeasurementAPI.Data;
using Microsoft.OpenApi.Models;
using QuantityMeasurementAPI.Services;
using QuantityMeasurementAPI.Middleware;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Memory Cache
builder.Services.AddMemoryCache();

// Add Auth Service
builder.Services.AddScoped<IAuthService, AuthService>();

// Add RabbitMQ Service (commented if RabbitMQ not installed)
builder.Services.AddSingleton<IMessageQueueService, RabbitMQService>();

// Add repository and service
builder.Services.AddScoped<IQuantityMeasurementRepository>(sp => 
    QuantityMeasurementCacheRepository.Instance);
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

// Get JWT configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? "DefaultKey32CharactersLongEnoughForDevelopment!";
var key = Encoding.ASCII.GetBytes(jwtKey);

// ✅ CORRECTED: Single AddAuthentication call with multiple authentication schemes
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Important for Google
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme) // Cookie scheme for Google OAuth
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"] ?? "";
    options.ClientSecret = builder.Configuration["Google:ClientSecret"] ?? "";
    options.CallbackPath = "/signin-google";
    options.SaveTokens = true;
    options.Scope.Add("profile");
    options.Scope.Add("email");
});

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Quantity Measurement API", 
        Version = "v1",
        Description = "REST API for quantity measurement operations with JWT authentication"
    });
    
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Add global exception handler
app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();  // Enable authentication
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Ok(new { message = "API is running!", timestamp = DateTime.UtcNow }));

app.Run();