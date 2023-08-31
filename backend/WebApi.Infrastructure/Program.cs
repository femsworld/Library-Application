using System.Security.Claims;
using JWT.Algorithms;
using JWT.Extensions.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Business;
using WebApi.Business.Middlewares;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Business.Services.Implementations;
using WebApi.Infrastructure.Database;
using WebApi.Infrastructure.RepoImplementations;
using AutoMapper;
using WebApi.Domain.Entities;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add Automapper DI
// builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Moved from DatabaseContext.cs here due npgslp version 4 recomendations not to create new npgsqldatasourcebuilder within the scope
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var npgsqlBuilder = new NpgsqlDataSourceBuilder(connectionString);
npgsqlBuilder.MapEnum<Role>();
npgsqlBuilder.MapEnum<Genre>();

// await using var dataSource = npgsqlBuilder.Build();
var dataSource = npgsqlBuilder.Build();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.AddInterceptors(new TimeStampInterceptor());
    options.UseNpgsql(dataSource)
           .UseSnakeCaseNamingConvention();
});

// Add service DI
builder.Services
.AddScoped<LoggingMiddleware>()
.AddScoped<ErrorHandlerMiddware>()
.AddScoped<IAuthService, AuthService>()
.AddScoped<IUserRepo, UserRepo>()
.AddScoped<IUserService, UserService>()
.AddScoped<ILoanRepo, LoanRepo>()
.AddScoped<ILoanService, LoanService>()
.AddScoped<ILoanBookRepo, LoanBookRepo>()
.AddScoped<ILoanBookService, LoanBookService>()
.AddScoped<IBookRepo, BookRepo>()
.AddScoped<IBookService, BookService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//Config route

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//authentication:
builder.Services.AddAuthentication(JwtAuthenticationDefaults.AuthenticationScheme)
                    .AddJwt(options =>
                    {
                        options.Keys = new[] { "my-secrete-key" };
                        options.VerifySignature = true;
                    });

builder.Services.AddSingleton<IAlgorithmFactory>(new HMACSHAAlgorithmFactory());

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

dataSource.Dispose();