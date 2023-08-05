using System.Security.Claims;
using JWT.Algorithms;
using JWT.Extensions.AspNetCore;
using WebApi.Business;
using WebApi.Business.Middlewares;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Business.Services.Implementations;
using WebApi.Infrastructure.Database;
using WebApi.Infrastructure.RepoImplementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<LoggingMiddleware>();
builder.Services.AddScoped<ErrorHandlerMiddware>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();

// builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
.AddAuthentication(JwtAuthenticationDefaults.AuthenticationScheme)
.AddJwt(
    options =>
    {
        options.Keys = new[] { "my-secrete-key" };
        options.VerifySignature = true;
    }
);

builder.Services.AddSingleton<IAlgorithmFactory>(new HMACSHAAlgorithmFactory());

builder.Services.AddAuthorization(options =>
{
    // options.AddPolicy("AdminEmail", policy => policy.RequireClaim(ClaimTypes.Email, "femi@mail.com"));
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();

app.UseMiddleware<ErrorHandlerMiddware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
