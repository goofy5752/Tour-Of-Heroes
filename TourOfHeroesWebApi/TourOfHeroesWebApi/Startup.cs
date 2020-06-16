// ReSharper disable StringLiteralTypo
namespace TourOfHeroesWebApi
{
    using System.IO;
    using System.Reflection;

    using TourOfHeroesData.Seeder.Contracts;
    using TourOfHeroesMapping.Mapping;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesServices.RealTimeHub;

    using NLog;
    using Infrastructure.Extensions;
    using GlobalErrorHandling.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) =>
            services
                .SetupMvc()
                .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
                .AddSwagger()
                .AddApplicationServices()
                .SetupSignalR();

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISeeder seeder)
        {
            AutoMapperConfig.RegisterMappings(typeof(PageResultDTO<>).GetTypeInfo().Assembly);

            //Seed database with roles, users, blogs, movies, etc..

            seeder.SeedDatabase();

            app
                .UseSwaggerUi()
                .UseDeveloperExceptionPage()
                .UseCors(options =>
                    options
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(Configuration["ApplicationSettings:Client_URL"]))
                .UseSignalR(routes =>
                {
                    routes.MapHub<CommentHub>("/api/comments");
                    routes.MapHub<CommentHub>("/api/blog");
                    routes.MapHub<ProfileImageHub>("/api/profile");
                })
                .UseHttpsRedirection()
                .UseHsts()
                .UseAuthentication()
                .UseMvc()
                .ConfigureCustomExceptionMiddleware();
        }
    }
}