// using System.Security.Claims;
// using JWT.Algorithms;
// using JWT.Extensions.AspNetCore;
// using Microsoft.EntityFrameworkCore;
// using Npgsql;
// using WebApi.Business;
// using WebApi.Business.Middlewares;
// using WebApi.Business.RepoAbstractions;
// using WebApi.Business.Services.Abstractions;
// using WebApi.Business.Services.Implementations;
// using WebApi.Domain.Entities;
// using WebApi.Infrastructure.Database;
// using WebApi.Infrastructure.RepoImplementations;

// var builder = WebApplication.CreateBuilder(args);

// // builder.Services.AddDbContext<DatabaseContext>();

// // Add services to the container.
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddScoped<LoggingMiddleware>();
// builder.Services.AddScoped<ErrorHandlerMiddware>();
// builder.Services.AddScoped<IUserService, UserService>();

// builder.Services.AddScoped<IBookService, BookService>();
// builder.Services.AddScoped<IUserRepo, UserRepo>();
// builder.Services.AddScoped<IBookRepo, BookRepo>();
// builder.Services.AddScoped<ILoanRepo, LoanRepo>();
// builder.Services.AddScoped<ILoanBookRepo, LoanBookRepo>();
// builder.Services.AddScoped<ILoanService, LoanService>();
// builder.Services.AddScoped<ILoanBookService, LoanBookService>();
// builder.Services.AddScoped<ICartService, CartService>();
// builder.Services.AddScoped<ICartRepo, CartRepo>();
// // builder.Services.AddTransient<ICartService, CartService>();
// // builder.Services.AddTransient<ICartRepo, CartRepo>();
// builder.Services.AddScoped<IAuthService, AuthService>();
// // builder.Services.AddAutoMapper(typeof(Program).Assembly);
// builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// var connectionString = builder.Configuration.GetConnectionString("Default");
// // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// var npgsqlBuilder = new NpgsqlDataSourceBuilder(connectionString);
// npgsqlBuilder.MapEnum<Role>();
// // npgsqlBuilder.MapEnum<OrderStatus>();
// await using var dataSource = npgsqlBuilder.Build();
// builder.Services.AddDbContext<DatabaseContext>(options =>
// {
//     options.AddInterceptors(new TimeStampInterceptor());
//     options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
//     // optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
// });

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// builder.Services
// .AddAuthentication(JwtAuthenticationDefaults.AuthenticationScheme)
// .AddJwt(
//     options =>
//     {
//         options.Keys = new[] { "my-secrete-key" };
//         options.VerifySignature = true;
//     }
// );

// // builder.Services.AddSingleton<IAlgorithmFactory>(new RS256AlgorithmFactory("<your RSA public key>"));

// builder.Services.AddSingleton<IAlgorithmFactory>(new HMACSHAAlgorithmFactory());

// builder.Services.AddAuthorization(options =>
// {
//     // options.AddPolicy("AdminEmail", policy => policy.RequireClaim(ClaimTypes.Email, "femi@mail.com"));
//     options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
// });

// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(builder =>
//     {
//         builder.WithOrigins("http://localhost:3000") // React app's URL
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//     });
// });

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// // if (app.Environment.IsDevelopment())
// // {
// //     app.UseSwagger();
// //     app.UseSwaggerUI();
// // }

// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseCors();

// app.UseMiddleware<LoggingMiddleware>();

// app.UseMiddleware<ErrorHandlerMiddware>();

// app.UseAuthentication();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();


using Microsoft.Extensions.Hosting;
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
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;
using WebApi.Infrastructure.RepoImplementations;
using AutoMapper;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices((hostContext, services) =>
        {
            var configuration = hostContext.Configuration;
            // Add your services here...
            services.AddControllers();
            services.AddScoped<LoggingMiddleware>();
            services.AddScoped<ErrorHandlerMiddware>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserRepo, UserRepo>();
            // services.AddScoped<IBookRepo, BookRepo>();
            // services.AddScoped<IBookRepo>(provider =>
            // new BookRepo(provider.GetRequiredService<DatabaseContext>(), configuration));

            services.AddScoped<IBookRepo>(provider =>
            new BookRepo(provider.GetRequiredService<DatabaseContext>(), provider.GetRequiredService<IMapper>()));

            services.AddScoped<ILoanRepo, LoanRepo>();
            services.AddScoped<ILoanBookRepo, LoanBookRepo>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoanBookService, LoanBookService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            // Configure your database
            // var connectionString = configuration.GetConnectionString("DefaultConnection");
            // var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            // npgsqlBuilder.TypeMapper.UseNetTopologySuite(); // Add this line for NpgsqlEnumMapping
            // npgsqlBuilder.TypeMapper.MapEnum<Role>("role"); // Add this line for NpgsqlEnumMapping
            // var dataSource = npgsqlBuilder.ConnectionString;

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            // npgsqlBuilder.MapEnum<Role>();
            // npgsqlBuilder.MapEnum<Genre>();

            // npgsqlBuilder.AddEnumMappings(new NpgsqlEnumMapping { ClrEnumType = typeof(Role), PgEnumName = "role" });
            // npgsqlBuilder.AddEnumMappings(new NpgsqlEnumMapping { ClrEnumType = typeof(Genre), PgEnumName = "genre" });


            var dataSource = npgsqlBuilder.ConnectionString;    

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.AddInterceptors(new TimeStampInterceptor());
                options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
                // Configure enums if necessary
                // options.UseEnum<Role>();
            });

            // Configure other services...
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication(JwtAuthenticationDefaults.AuthenticationScheme)
                    .AddJwt(options =>
                    {
                        options.Keys = new[] { "my-secrete-key" };
                        options.VerifySignature = true;
                    });

            services.AddSingleton<IAlgorithmFactory>(new HMACSHAAlgorithmFactory());

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        })
        .Configure(app =>
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddware>();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        });
    })
    .Build();

host.Run();
