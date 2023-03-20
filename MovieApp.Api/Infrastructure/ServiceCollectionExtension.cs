using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MovieApp.Api.Common;
using MovieApp.Api.Data;
using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;
using MovieApp.Api.Data.Repositories;
using System.Text;
using System.Text.Json.Serialization;
using System.Web.Http.ExceptionHandling;

namespace MovieApp.Api.Infrastructure
{
    /// <summary>
    /// Extension to the IServiceCollection to handle services changes needed for the App. Functionality includes:
    /// Adding DbContext, scoping repositories, authenticating using JWT tokens, setting rate limits, seeding data
    /// </summary>
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Db Context and scoped repositories
            services.AddDbContext<MovieContext>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IActorsRepository, ActorsRepository>();
            services.AddScoped<IMovieRatingsRepository, MovieRatingsRepository>();
            services.AddScoped<IMovieToActorsRepository, MovieToActorsRepository>();

            //Allow JsonSerializer to preserve references and handle circular reference
            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //Add authentication with jwt bearer token
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetValue<string>(AppConstants.Configurations.JWTValidIssue),
                    ValidAudience = configuration.GetValue<string>(AppConstants.Configurations.JWTValidAudience),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>(AppConstants.Configurations.JWTSecret)))
                };
            });

            // Add logic to set rate limit
            // Used to store rate limit counters and ip rules
            services.AddMemoryCache();

            // Load in general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(options => configuration.GetSection(AppConstants.Configurations.IpRateLimitingSettings).Bind(options));

            // Inject Counter and Store Rules
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            //Seed Data based on configuration
            if (configuration.GetValue<bool>(AppConstants.Configurations.AppStartUpSeedData))
            {
                DbSetup.SeedData();
            }
            
            return services;
        }
    }
}
