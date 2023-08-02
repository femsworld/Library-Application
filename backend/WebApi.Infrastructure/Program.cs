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
// builder.Services.AddScoped<ErrorHandlerMiddware>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();

// builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggingMiddleware>();

// app.UseMiddleware<ErrorHandlerMiddware>();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
