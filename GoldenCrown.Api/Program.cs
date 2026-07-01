using FluentValidation;
using GoldenCrown.Api.BackGroundServices;
using GoldenCrown.Api.Middlewares;
using GoldenCrown.Application.Dtos.User;
using GoldenCrown.Application.Features.User.UserLogin;
using GoldenCrown.Application.Services.Currency;
using GoldenCrown.Infrastructure.Clients.ExchangeClient;
using GoldenCrown.Infrastructure.Clients.ExchangeClient.Models;
using GoldenCrown.Infrastructure.Database;
using GoldenCrown.Infrastructure.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.OpenApi;

namespace GoldenCrown.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException(
                                       "Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(UserLoginCommandHandler).Assembly);
            });

            builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.Configure<ExchangeClientSettings>(builder.Configuration.GetSection("ExchangeClient"));

            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ExchangeClient>();
            builder.Services.AddScoped<IExchangeClient, DistributedCachedExchangeClient>(sp =>
                new DistributedCachedExchangeClient(
                    sp.GetRequiredService<ExchangeClient>(),
                    sp.GetRequiredService<IDistributedCache>(),
                    sp.GetRequiredService<ILogger<DistributedCachedExchangeClient>>()
                ));

            builder.Services.AddSingleton<IMessageProducer, RabbitMqMessageProducer>();
            
            builder.Services.AddValidatorsFromAssemblyContaining<LoginRequest>();
            builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);

            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = builder.Configuration["Redis:Configuration"];
                o.InstanceName = builder.Configuration["Redis:InstanceName"];
            });
            
            builder.Services.AddHostedService<SessionCleanupService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Please enter into field your api token"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });
            
            var app = builder.Build();
            
            MigrateDatabase(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<AuthorizationMiddleware>();

            app.MapControllers();
            
            app.Run();
        }

        private static void MigrateDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }
    }
}