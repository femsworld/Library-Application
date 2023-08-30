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

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices((hostContext, services) =>
        {
            var configuration = hostContext.Configuration;
            services.AddControllers();
            services.AddScoped<LoggingMiddleware>();
            services.AddScoped<ErrorHandlerMiddware>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ILoanRepo, LoanRepo>();
            services.AddScoped<ILoanBookRepo, LoanBookRepo>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoanBookService, LoanBookService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookRepo>(provider =>
                new BookRepo(provider.GetRequiredService<DatabaseContext>(), provider.GetRequiredService<IMapper>()));

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
           
            services.AddScoped<IMapper>(provider =>
            {
                var mapperConfig = new MapperConfiguration(config =>
                {
                    config.AddProfile<AutoMapperProfile>(); // Replace with the actual AutoMapper profile class
                });

                return mapperConfig.CreateMapper();
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            
            var dataSource = npgsqlBuilder.ConnectionString;    

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.AddInterceptors(new TimeStampInterceptor());
                options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
            });

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
                    builder.WithOrigins("http://localhost:3000", 
                            "https://spontaneous-bubblegum-f213e6.netlify.app/" )
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
