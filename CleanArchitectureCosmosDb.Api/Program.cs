using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using CleanArchitectureCosmosDb.Application.Services;
using CleanArchitectureCosmosDb.Domain.Interfaces;
using CleanArchitectureCosmosDb.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.OpenApi.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture CosmosDB API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            new string[] { }
        }
    });
});

// Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure Cosmos DB client
builder.Services.AddSingleton<CosmosClient>(s =>
{
    var connectionString = builder.Configuration.GetConnectionString("CosmosDb");
    return new CosmosClient(connectionString);
});

// Register repositories
builder.Services.AddTransient<IUserRepository>(s =>
{
    var cosmosClient = s.GetRequiredService<CosmosClient>();
    var databaseName = builder.Configuration["CosmosDb:DatabaseName"];
    var containerName = "Users"; // Replace with your actual container name
    return new UserRepository(cosmosClient, databaseName, containerName);
});

builder.Services.AddTransient<IProgramRepository>(s =>
{
    var cosmosClient = s.GetRequiredService<CosmosClient>();
    var databaseName = builder.Configuration["CosmosDb:DatabaseName"];
    var containerName = "Programs"; // Replace with your actual container name
    return new ProgramRepository(cosmosClient, databaseName, containerName);
});

// JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Secret"));

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Override the default clock skew of 5 mins
        };
    });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployerPolicy", policy => policy.RequireRole("Employer"));
    options.AddPolicy("CandidatePolicy", policy => policy.RequireRole("Candidate"));
});

// Register services
builder.Services.AddTransient<ProgramService>();
builder.Services.AddTransient<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture CosmosDB API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
