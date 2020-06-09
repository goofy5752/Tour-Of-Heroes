namespace TourOfHeroesWebApi.Infrastructure.Extensions
{
    using System;
    using System.Text;

    using TourOfHeroesData;
    using TourOfHeroesData.Models;
    using TourOfHeroesData.Seeder;
    using TourOfHeroesData.Seeder.Contracts;
    using TourOfHeroesServices;
    using TourOfHeroesServices.Contracts;
    using TourOfHeroesData.Common;
    using TourOfHeroesData.Common.Contracts;

    using Controllers.Validator;
    using Controllers.Validator.Contracts;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionExtensions
    {
        public static ApplicationSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<ApplicationSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<ApplicationSettings>();
        }

        public static IServiceCollection SetupMvc(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<TourOfHeroesDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TourOfHeroesDbContext>();

            services.Configure<IdentityOptions>(IdentityOptionsProvider.GetIdentityOptions);

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            ApplicationSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.JWT_Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection SetupSignalR(this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddTransient<ISeeder, Seeder>()
                .AddTransient<IImageService, ImageService>()
                .AddTransient<IHeroService, HeroService>()
                .AddTransient<IHistoryService, HistoryService>()
                .AddTransient<IMovieService, MovieService>()
                .AddTransient<IProfileService, ProfileService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IBlogService, BlogService>()
                .AddTransient<IUserService, UserService>()
                .AddSingleton<ILoggerManager, LoggerManager>()
                .AddTransient<IUserValidator, UserValidator>()
                .AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }
}