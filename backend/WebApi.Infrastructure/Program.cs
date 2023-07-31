using WebApi.Business.Middlewares;
using WebApi.Business.Services.Abstractions;
using WebApi.Business.Services.Implementations;
using WebApi.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<LoggingMiddleware>();
builder.Services.AddScoped<ErrorHandlerMiddware>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

app.UseMiddleware<ErrorHandlerMiddware>();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
